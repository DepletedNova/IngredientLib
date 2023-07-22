using IngredientLib.Repair.Components;
using IngredientLib.Repair.GDOs;
using System.Linq;
using Unity.Collections;
using Unity.Entities;

namespace IngredientLib.Repair.Systems
{
    [UpdateAfter(typeof(EndOfDayProgressionGroup))]
    internal class CreateRepairMenuTrigger : NightSystem, IModSystem
    {
        private EntityQuery Appliances;
        private EntityQuery Unlocks;
        private EntityQuery Menus;
        private EntityQuery Ingredients;
        private EntityQuery Extras;
        private EntityQuery Blockers;

        protected override void Initialise()
        {
            base.Initialise();

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
            if (!HasSingleton<SCheckForRepair>() || HasSingleton<SIsNightFirstUpdate>())
                return;

            if (HasSingleton<SCheckForRepair>() && HasSingleton<SRepair>())
            {
                EntityManager.DestroyEntity(GetSingletonEntity<SCheckForRepair>());
                return;
            }

            if (Unlocks.IsEmpty || Menus.IsEmpty || Ingredients.IsEmpty || Appliances.IsEmpty)
                return;

            Main.LogInfo("Attempting to diagnose dish issues for the current session...");

            var IsBroke = false;

            List<int> RequiredItems = new();
            List<int> RequiredProcesses = new();
            List<int> BlockedItems = new();
            List<(int Menu, int Ingredient)> RequiredIngredients = new();
            List<(int Menu, int Ingredient)> RequiredExtras = new();

            // Collect requirements
            var unlocks = Unlocks.ToComponentDataArray<CProgressionUnlock>(Allocator.Temp);
            foreach (var unlock in unlocks)
            {
                if (GameData.Main.TryGet(unlock.ID, out Dish dishUnlock))
                {
                    // Required Items
                    foreach (var item in dishUnlock.MinimumIngredients)
                    {
                        if (item.DedicatedProvider == null)
                            continue;

                        if (item.DedicatedProvider.GetProperty(out CDynamicItemProvider _) || !item.DedicatedProvider.GetProperty(out CItemProvider cProvider))
                            continue;

                        if (!RequiredItems.Contains(cProvider.DefaultProvidedItem))
                            RequiredItems.Add(cProvider.DefaultProvidedItem);
                    }

                    // Required Processes
                    foreach (var process in dishUnlock.RequiredProcesses)
                    {
                        var id = process.ID;
                        if (process.IsPseudoprocessFor != null)
                            id = process.IsPseudoprocessFor.ID;

                        if (!RequiredProcesses.Contains(id))
                            RequiredProcesses.Add(id);
                    }
                    
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
                        {
                            foreach (var itemSet in itemGroup.DerivedSets)
                                if (!itemSet.RequiresUnlock)
                                    foreach (var item in itemSet.Items)
                                        if (!RequiredIngredients.Contains((itemGroup.ID, item.ID)))
                                            RequiredIngredients.Add((itemGroup.ID, item.ID));
                        }
                    }

                    // Extras
                    foreach (var unlockedExtra in dishUnlock.ExtraOrderUnlocks)
                        if (!RequiredExtras.Contains((unlockedExtra.MenuItem.ID, unlockedExtra.Ingredient.ID)))
                            RequiredExtras.Add((unlockedExtra.MenuItem.ID, unlockedExtra.Ingredient.ID));
                }
            }
            unlocks.Dispose();

            Main.LogInfo("=====");

            Main.LogInfo($"Required items: {RequiredItems.Count}");
            Main.LogInfo($"Required processes: {RequiredProcesses.Count}");
            Main.LogInfo($"Blocked items: {BlockedItems.Count}");
            Main.LogInfo($"Unlocked ingredients: {RequiredIngredients.Count}");
            Main.LogInfo($"Extras: {RequiredExtras.Count}");

            Main.LogInfo("=====");

            // Check requirements

            // Appliances
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
                    {
                        if (RequiredProcesses.Contains(process.Process.ID))
                            RequiredProcesses.Remove(process.Process.ID);
                        if (process.Process.IsPseudoprocessFor != null && RequiredProcesses.Contains(process.Process.IsPseudoprocessFor.ID))
                            RequiredProcesses.Remove(process.Process.IsPseudoprocessFor.ID);
                    }
                }
            }
            IsBroke |= RequiredItems.Count > 0 || RequiredProcesses.Count > 0;
            if (RequiredItems.Count > 0) Main.LogInfo($"Missing required items: {RequiredItems.Count}");
            else Main.LogInfo("No missing required items");
            if (RequiredProcesses.Count > 0) Main.LogInfo($"Missing required processes: {RequiredProcesses.Count}");
            else Main.LogInfo("No missing required processes");
            applianceEntities.Dispose();

            // Blockers
            var blockers = Blockers.ToComponentDataArray<CBlockedIngredient>(Allocator.Temp);
            foreach (var blocker in blockers)
                if (BlockedItems.Contains(blocker.Item))
                    BlockedItems.Remove(blocker.Item);
            IsBroke |= BlockedItems.Count > 0;
            if (BlockedItems.Count > 0) Main.LogInfo($"Missing blockers: {BlockedItems.Count}");
            else Main.LogInfo("No missing blockers");
            blockers.Dispose();

            // Ingredients
            var ingredients = Ingredients.ToComponentDataArray<CAvailableIngredient>(Allocator.Temp);
            int excessIngredients = 0;
            foreach (var ingredient in ingredients)
            {
                if (ingredient.MenuItem == 0 || ingredient.Ingredient == 0) continue;

                var index = RequiredIngredients.FindIndex(x => ingredient.Ingredient == x.Ingredient && ingredient.MenuItem == x.Menu);
                if (index != -1) RequiredIngredients.RemoveAt(index);
                else excessIngredients++;
            }
            IsBroke |= excessIngredients > 0 || RequiredIngredients.Count > 0;
            if (RequiredIngredients.Count > 0) Main.LogInfo($"Missing unlocked ingredients: {RequiredIngredients.Count}");
            else Main.LogInfo("No missing unlocked ingredients");
            if (excessIngredients > 0) Main.LogInfo($"Excess unlocked ingredients: {excessIngredients}");
            else Main.LogInfo("No excess unlocked ingredients");
            ingredients.Dispose();

            // Extras
            var extras = Extras.ToComponentDataArray<CPossibleExtra>(Allocator.Temp);
            var excessExtras = 0;
            foreach (var extra in extras)
            {
                if (extra.MenuItem == 0 || extra.Ingredient == 0) continue;

                var index = RequiredExtras.FindIndex(x => extra.Ingredient == x.Ingredient && extra.MenuItem == x.Menu);
                if (index != -1) RequiredExtras.RemoveAt(index);
                else excessExtras++;
            }
            IsBroke |= excessExtras > 0 || RequiredExtras.Count > 0;
            if (RequiredExtras.Count > 0) Main.LogInfo($"Missing extras: {RequiredExtras.Count}");
            else Main.LogInfo("No missing extras");
            if (excessExtras > 0) Main.LogInfo($"Excess extras: {excessExtras}");
            else Main.LogInfo("No excess extras");
            extras.Dispose();

            // Menu phase
            var menuEntities = Menus.ToEntityArray(Allocator.Temp);
            int invalidCourses = 0;
            foreach (var entity in menuEntities)
                if (Require(entity, out CMenuItem cMenu) && GameData.Main.TryGet(cMenu.SourceDish, out Dish dish))
                    if (!dish.UnlocksMenuItems.Any(p => p.Item.ID == cMenu.Item && p.Phase == cMenu.Phase))
                        invalidCourses++;
            IsBroke |= invalidCourses > 0;
            if (invalidCourses > 0) Main.LogInfo($"Mismatched courses: {invalidCourses}");
            else Main.LogInfo("No mismatched courses");
            menuEntities.Dispose();

            Main.LogInfo("=====");

            if (IsBroke)
            {
                Main.LogInfo("Spawning repair trigger");

                Vector3 frontDoor = GetFrontDoor();
                var entity = EntityManager.CreateEntity(new ComponentType[]
                {
                    typeof(CCreateAppliance),
                    typeof(CPosition),
                    typeof(SRepair)
                });

                EntityManager.SetComponentData(entity, new CCreateAppliance { ID = RepairMenuTrigger.ApplianceID });
                EntityManager.SetComponentData(entity, new CPosition(frontDoor + new Vector3(frontDoor.x > 0f ? -5 : 5, 0f, -1f)));
            }
            else
            {
                Main.LogInfo("Skipping repair trigger");
            }

            EntityManager.DestroyEntity(GetSingletonEntity<SCheckForRepair>());
        }
    }
}
