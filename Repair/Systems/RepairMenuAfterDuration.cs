using IngredientLib.Repair.Components;
using Kitchen;
using KitchenData;
using Unity.Collections;
using Unity.Entities;
using static KitchenData.Dish;
using static Unity.Collections.AllocatorManager;

namespace IngredientLib.Repair.Systems
{
    public class RepairMenuAfterDuration : GameSystemBase, IModSystem
    {
        private EntityQuery RepairTriggers;

        private EntityQuery Appliances;
        private EntityQuery Unlocks;
        private EntityQuery Menus;
        private EntityQuery Ingredients;
        private EntityQuery Extras;
        private EntityQuery Blockers;

        protected override void Initialise()
        {
            base.Initialise();
            RepairTriggers = GetEntityQuery(new QueryHelper()
                .All(typeof(CAppliance), typeof(CRepairAfterDuration), typeof(CTakesDuration)));

            Appliances = GetEntityQuery(new QueryHelper()
                .Any(typeof(CAppliance), typeof(CCreateAppliance), typeof(CLetterAppliance)));
            Unlocks = GetEntityQuery(new QueryHelper()
                .All(typeof(CProgressionUnlock)));
            Menus = GetEntityQuery(new QueryHelper()
                .All(typeof(CMenuItem)));
            Ingredients = GetEntityQuery(new QueryHelper()
                .All(typeof(CAvailableIngredient))
                .None(typeof(CMenuItem)));
            Extras = GetEntityQuery(new QueryHelper()
                .All(typeof(CPossibleExtra)));
            Blockers = GetEntityQuery(new QueryHelper()
                .All(typeof(CBlockedIngredient)));
        }

        protected override void OnUpdate()
        {
            EntityCommandBuffer ECB = new(Allocator.Temp);

            var entities = RepairTriggers.ToEntityArray(Allocator.Temp);
            foreach (var trigger in entities)
            {
                var cDuration = GetComponent<CTakesDuration>(trigger);
                if (cDuration.Remaining > 0f || !cDuration.Active)
                    continue;

                Main.LogInfo("[Repair] Attempting to fix corrupted modded dishes...");

                List<(MenuItem Menu, int Source) > RequiredMenus = new();
                List<int> RequiredItems = new();
                List<int> RequiredProcesses = new();
                List<int> BlockedItems = new();
                List<(int Menu, int Ingredient)> RequiredIngredients = new();
                List<(int Menu, int Ingredient)> RequiredExtras = new();

                #region Collection
                var unlocks = Unlocks.ToComponentDataArray<CProgressionUnlock>(Allocator.Temp);
                foreach (var unlock in unlocks)
                {
                    if (GameData.Main.TryGet(unlock.ID, out Dish dishUnlock))
                    {
                        // Required Items
                        foreach (var item in dishUnlock.MinimumIngredients)
                            if (!RequiredItems.Contains(item.ID))
                                RequiredItems.Add(item.ID);

                        // Required Processes
                        foreach (var process in dishUnlock.RequiredProcesses)
                            if (!RequiredProcesses.Contains(process.ID))
                                RequiredProcesses.Add(process.ID);

                        // Blocked items
                        foreach (var blocked in dishUnlock.BlockProviders)
                        {
                            if (!BlockedItems.Contains(blocked.ID))
                                BlockedItems.Add(blocked.ID);
                            if (RequiredItems.Contains(blocked.ID))
                                RequiredItems.Remove(blocked.ID);
                        }

                        // Ingredient Unlocks
                        foreach (var unlockedIngredient in dishUnlock.UnlocksIngredients)
                            if (!RequiredIngredients.Contains((unlockedIngredient.MenuItem.ID, unlockedIngredient.Ingredient.ID)))
                                RequiredIngredients.Add((unlockedIngredient.MenuItem.ID, unlockedIngredient.Ingredient.ID));

                        foreach (var menuItem in dishUnlock.UnlocksMenuItems)
                        {
                            var itemGroup = menuItem.Item as ItemGroup;
                            if (itemGroup != null)
                                foreach (var itemSet in itemGroup.DerivedSets)
                                    if (!itemSet.RequiresUnlock)
                                        foreach (var item in itemSet.Items)
                                            if (!RequiredIngredients.Contains((itemGroup.ID, item.ID)))
                                                RequiredIngredients.Add((itemGroup.ID, item.ID));

                            RequiredMenus.Add((menuItem, unlock.ID));
                        }

                        // Extras
                        foreach (var unlockedExtra in dishUnlock.ExtraOrderUnlocks)
                            if (!RequiredExtras.Contains((unlockedExtra.MenuItem.ID, unlockedExtra.Ingredient.ID)))
                                RequiredExtras.Add((unlockedExtra.MenuItem.ID, unlockedExtra.Ingredient.ID));
                    }
                }
                unlocks.Dispose();
                #endregion

                Main.LogInfo("[Repair] Collected dish info...");
                Main.LogInfo("[Repair] =====");

                #region Appliances
                // Gather missing processes/providers
                var applianceEntities = Appliances.ToEntityArray(Allocator.Temp);
                foreach (var entity in applianceEntities)
                {
                    int id = Require(entity, out CLetterAppliance cLetter) ? cLetter.ApplianceID :
                        (Require(entity, out CAppliance cAppliance) ? cAppliance.ID :
                        (Require(entity, out CCreateAppliance cCreate) ? cCreate.ID : 0));
                    if (GameData.Main.TryGet(id, out Appliance appliance))
                    {
                        if (appliance.GetProperty(out CItemProvider cProvider) && cProvider.DefaultProvidedItem != 0)
                            if (RequiredItems.Contains(cProvider.DefaultProvidedItem))
                                RequiredItems.Remove(cProvider.DefaultProvidedItem);

                        foreach (var process in appliance.Processes)
                            if (RequiredProcesses.Contains(process.Process.ID))
                                RequiredProcesses.Remove(process.Process.ID);
                    }
                }
                applianceEntities.Dispose();
                // Add missing processes/providers
                var postTiles = GetPostTiles();
                int postOffset = 0;
                if (RequiredProcesses.Count > 0)
                {
                    foreach (var processID in RequiredProcesses)
                    {
                        if (!GameData.Main.TryGet(processID, out Process process))
                            continue;

                        var appliance = process.BasicEnablingAppliance as Appliance;
                        if (appliance == null)
                            continue;

                        if (appliance.GetProperty(out CItemProvider cProvider) && RequiredItems.Contains(cProvider.DefaultProvidedItem))
                            RequiredItems.Remove(cProvider.DefaultProvidedItem);

                        Main.LogInfo($"[Repair] Spawning appliance(s): {appliance.ID}");
                        for (int i = 0; i < process.EnablingApplianceCount; i++)
                            PostHelpers.CreateApplianceParcel(EntityManager, GetParcelTile(postTiles, ref postOffset), appliance.ID);
                    }
                }
                if (RequiredItems.Count > 0)
                {
                    foreach (var itemID in RequiredItems)
                    {
                        if (!GameData.Main.TryGet(itemID, out Item item) || 
                            item.DedicatedProvider == null)
                            continue;

                        Main.LogInfo($"[Repair] Spawning appliance: {item.DedicatedProvider.ID}");
                        PostHelpers.CreateApplianceParcel(EntityManager, GetParcelTile(postTiles, ref postOffset), item.DedicatedProvider.ID);
                    }
                }
                #endregion

                #region Blockers
                var blockerEntities = Blockers.ToEntityArray(Allocator.Temp);
                foreach (var blockerEntity in blockerEntities)
                {
                    var blocker = GetComponent<CBlockedIngredient>(blockerEntity);
                    if (BlockedItems.Contains(blocker.Item))
                        BlockedItems.Remove(blocker.Item);
                    else
                    {
                        Main.LogInfo($"[Repair] Destroying blocker: {blocker.Item}");
                        ECB.DestroyEntity(blockerEntity);
                    }
                }
                foreach (var blocked in BlockedItems)
                {
                    Main.LogInfo($"[Repair] Creating blocker for: {blocked}");
                    var newBlocker = EntityManager.CreateEntity();
                    Set(newBlocker, new CBlockedIngredient()
                    {
                        Item = blocked
                    });
                }
                blockerEntities.Dispose();
                #endregion

                #region Ingredients
                var ingredientEntities = Ingredients.ToEntityArray(Allocator.Temp);
                foreach (var ingredientEntity in ingredientEntities)
                {
                    var ingredient = GetComponent<CAvailableIngredient>(ingredientEntity);
                    if (ingredient.MenuItem == 0 || ingredient.Ingredient == 0) continue;

                    var index = RequiredIngredients.FindIndex(x => ingredient.Ingredient == x.Ingredient && ingredient.MenuItem == x.Menu);
                    if (index != -1)
                        RequiredIngredients.RemoveAt(index);
                    else
                    {
                        Main.LogInfo($"[Repair] Destroying excess ingredient unlock: {ingredient.Ingredient} - {ingredient.MenuItem}");
                        ECB.DestroyEntity(ingredientEntity);
                    }
                }
                foreach (var unlockedIngredient in RequiredIngredients)
                {
                    Main.LogInfo($"[Repair] Creating ingredient unlock: {unlockedIngredient.Ingredient} - {unlockedIngredient.Menu}");
                    var iuEntity = EntityManager.CreateEntity();
                    Set(iuEntity, new CAvailableIngredient
                    {
                        Ingredient = unlockedIngredient.Ingredient,
                        MenuItem = unlockedIngredient.Menu
                    });
                }
                ingredientEntities.Dispose();
                #endregion

                #region Extras
                var extrasEntities = Extras.ToEntityArray(Allocator.Temp);
                foreach (var extraEntity in extrasEntities)
                {
                    var extra = GetComponent<CPossibleExtra>(extraEntity);
                    if (extra.MenuItem == 0 || extra.Ingredient == 0) continue;

                    var index = RequiredExtras.FindIndex(x => extra.Ingredient == x.Ingredient && extra.MenuItem == x.Menu);
                    if (index != -1)
                        RequiredExtras.RemoveAt(index);
                    else
                    {
                        Main.LogInfo($"[Repair] Destroying excess extra: {extra.Ingredient} - {extra.MenuItem}");
                        ECB.DestroyEntity(extraEntity);
                    }
                }
                foreach (var extra in RequiredExtras)
                {
                    Main.LogInfo($"[Repair] Creating ingredient unlock: {extra.Ingredient} - {extra.Menu}");
                    var eEntity = EntityManager.CreateEntity();
                    Set(eEntity, new CPossibleExtra
                    {
                        Ingredient = extra.Ingredient,
                        MenuItem = extra.Menu
                    });
                }
                extrasEntities.Dispose();
                #endregion

                #region Menu Phase
                var menuEntities = Menus.ToEntityArray(Allocator.Temp);
                foreach (var entity in menuEntities)
                {
                    var cMenu = GetComponent<CMenuItem>(entity);
                    if (!GameData.Main.TryGet(cMenu.SourceDish, out Dish dish) || !GameData.Main.TryGet(cMenu.Item, out Item item))
                    {
                        Main.LogInfo($"[Repair] Deleting invalid menu: {cMenu.SourceDish} - {cMenu.Item}");
                        ECB.DestroyEntity(entity);
                        continue;
                    }

                    var index = RequiredMenus.FindIndex(x => x.Menu.Item.ID == cMenu.Item && x.Source == cMenu.SourceDish);
                    if (index != -1)
                    {
                        if (RequiredMenus[index].Menu.Phase != cMenu.Phase)
                        {
                            Main.LogInfo($"[Repair] Correcting menu phase: {cMenu.SourceDish} - {cMenu.Item}");

                            var newPhase = RequiredMenus[index].Menu.Phase;

                            switch(cMenu.Phase)
                            {
                                case MenuPhase.Starter:
                                    EntityManager.RemoveComponent<CMenuItemStarter>(entity); break;
                                case MenuPhase.Main:
                                    EntityManager.RemoveComponent<CMenuItemMain>(entity); break;
                                case MenuPhase.Dessert:
                                    EntityManager.RemoveComponent<CMenuItemDessert>(entity); break;
                                case MenuPhase.Side:
                                    EntityManager.RemoveComponent<CMenuItemSide>(entity); break;
                            }

                            cMenu.Phase = newPhase;
                            EntityManager.SetComponentData(entity, cMenu);

                            switch (newPhase)
                            {
                                case MenuPhase.Starter:
                                    EntityManager.AddComponent<CMenuItemStarter>(entity); break;
                                case MenuPhase.Main:
                                    EntityManager.AddComponent<CMenuItemMain>(entity); break;
                                case MenuPhase.Dessert:
                                    EntityManager.AddComponent<CMenuItemDessert>(entity); break;
                                case MenuPhase.Side:
                                    EntityManager.AddComponent<CMenuItemSide>(entity); break;
                            }
                        }

                        RequiredMenus.RemoveAt(index);
                    }
                    else
                    {
                        Main.LogInfo($"[Repair] Deleting excess menu: {cMenu.SourceDish} - {cMenu.Item}");
                        ECB.DestroyEntity(entity);
                    }
                }
                foreach (var menuSet in RequiredMenus)
                {
                    Main.LogInfo($"[Repair] Creating menu: {menuSet.Source} - {menuSet.Menu.Item}");
                    var newMenu = EntityManager.CreateEntity(new ComponentType[] {
                        typeof(CMenuItem),
                        typeof(CAvailableIngredient)
                    });
                    EntityManager.AddComponentData(newMenu, new CMenuItem
                    {
                        Item = menuSet.Menu.Item.ID,
                        Weight = menuSet.Menu.Weight,
                        Phase = menuSet.Menu.Phase,
                        SourceDish = menuSet.Source
                    });
                    if (menuSet.Menu.DynamicMenuType > DynamicMenuType.Static)
                    {
                        EntityManager.AddComponentData(newMenu, new CDynamicMenuItem
                        {
                            Type = menuSet.Menu.DynamicMenuType,
                            Ingredient = menuSet.Menu.DynamicMenuIngredient.ID
                        });
                    }
                    switch (menuSet.Menu.Phase)
                    {
                        case MenuPhase.Starter:
                            EntityManager.AddComponent<CMenuItemStarter>(newMenu); break;
                        case MenuPhase.Main:
                            EntityManager.AddComponent<CMenuItemMain>(newMenu); break;
                        case MenuPhase.Dessert:
                            EntityManager.AddComponent<CMenuItemDessert>(newMenu); break;
                        case MenuPhase.Side:
                            EntityManager.AddComponent<CMenuItemSide>(newMenu); break;
                    }
                }
                menuEntities.Dispose();
                #endregion

                // Remove trigger
                ECB.DestroyEntity(trigger);

                Main.LogInfo("[Repair] =====");
                Main.LogInfo("[Repair] Successfully completed repairs");
                break;
            }

            ECB.Playback(EntityManager);
            ECB.Dispose();
        }

        private Vector3 GetParcelTile(List<Vector3> tiles, ref int offset)
        {
            Vector3 vector = Vector3.zero;
            bool flag = false;
            while (!flag && offset < tiles.Count)
            {
                int num = offset;
                offset = num + 1;
                vector = tiles[num];
                bool flag2 = GetOccupant(vector, OccupancyLayer.Default) == default(Entity) && !GetTile(vector).HasFeature;
                if (flag2)
                {
                    flag = true;
                }
            }
            bool flag3 = flag;
            Vector3 result;
            if (flag3)
            {
                result = vector;
            }
            else
            {
                result = GetFallbackTile();
            }
            return result;
        }
    }
}
