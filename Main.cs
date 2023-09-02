global using HarmonyLib;
global using IngredientLib.Ingredient.Items;
global using IngredientLib.Ingredient.Providers;
global using IngredientLib.Util;
global using IngredientLib.Util.Custom;
global using Kitchen;
global using Kitchen.Components;
global using KitchenData;
global using KitchenLib;
global using KitchenLib.Customs;
global using KitchenLib.Event;
global using KitchenLib.References;
global using KitchenLib.Utils;
global using KitchenMods;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Reflection;
global using UnityEngine;
global using UnityEngine.VFX;
global using static IngredientLib.References;
global using static IngredientLib.Util.Helper;
global using static KitchenData.ItemGroup;
global using static KitchenLib.Utils.GameObjectUtils;
global using static KitchenLib.Utils.GDOUtils;
global using static KitchenLib.Utils.KitchenPropertiesUtils;
global using static KitchenLib.Utils.MaterialUtils;
using Controllers;
using IngredientLib.Menu;
using IngredientLib.Repair.Patches;
using KitchenLib.Preferences;
using System.IO;
using TMPro;

namespace IngredientLib
{
    public class Main : BaseMod
    {
        public const string GUID = "ingredientlib";
        public const string VERSION = "1.2.5";

        public Main() : base(GUID, "IngredientLib", "Depleted Supernova#1957", VERSION, ">=1.1.0", Assembly.GetExecutingAssembly()) { }

        #region References
        public static AssetBundle Bundle;

        internal static StartDayWarning CorruptedSaveWarning = (StartDayWarning)VariousUtils.GetID("IL:CorruptedSave");

        internal static PauseMenuAction RepairAction = (PauseMenuAction)VariousUtils.GetID("IL:RepairMenu");
        internal static GameStateRequest RepairRequest = (GameStateRequest)VariousUtils.GetID("IL:RepairMenu");

        public static bool StartPractice(WarningLevel level) => level.IsBlocking() || SStartDayWarnings_Patch.CorruptedSave.IsBlocking();
        #endregion

        #region Visuals
        private void AddMaterials()
        {
            // Common
            AddTransparent("Glass", 0xF6FEFF, 0.3f);
            AddTransparent("Plastic - Transparent", 0xF6FEFF, 0.7f);

            AddFlat("Sack - Brown", 0xb59b7c);

            AddFlat("Stem", 0x6B3E26);
            AddFlat("White Fruit", 0xE5FFDC);

            AddFlat("Emoji Yellow", 0xFAC036);
            AddFlat("Emoji Orange", 0xE48C15);

            // Specific
            AddFlat("Vinegar", 0xFFF0C6);

            AddFlat("Chocolate", 0x7B3F00);
            AddFlat("Chocolate Dark", 0x753B00);
            AddFlat("Chocolate Light", 0x854400);

            AddFlat("Butter", 0xFAF28D);

            AddFlat("Banana", 0xFFFBC9);
            AddFlat("Banana Peel", 0xFFE135);
            AddFlat("Banana Inner", 0xE5DE6E);

            AddFlat("Lemon", 0xffe200);
            AddFlat("Lemon Inner", 0xCCBB34);
            AddFlat("Lemon Juice", 0xFFF26D);

            AddFlat("Lime", 0x549231);
            AddFlat("Lime Inner", 0x497109);
            AddFlat("Lime Juice", 0xBCE896);

            AddFlat("Oat Mound", 0xdfc496);
            AddFlat("Oat Grain", 0xd8c29d);

            AddFlat("Pork Fat", 0xFFCBCB);
            AddFlat("Pork", 0xE291AC);
            AddFlat("Porkchop Fat", 0xEADCB3);
            AddFlat("Porkchop", 0xB38F5A);
            AddFlat("Bacon Fat", 0xC5A099);
            AddFlat("Bacon", 0x8E3D2F);

            AddFlat("Raw Chicken", 0xFFBEBE);
            AddFlat("Cooked Chicken", 0xB95B24);

            AddFlat("Cooked Drumstick", 0xB65B28);
            AddFlat("Raw Drumstick Bone", 0xC9B9B4);
            AddFlat("Cooked Drumstick Bone", 0xC69067);

            AddFlat("Honey", 0xEBA937);

            AddFlat("Egg Dough", 0xF4E2AA);

            AddFlat("Caramel", 0xC68E17);

            AddFlat("Cinnamon", 0xC58C66);

            AddFlat("Garlic", 0xf2e9d2);

            AddFlat("Jalapeno", 0x548042);

            AddFlat("Blueberry", 0x4f86f7);
            AddFlat("Blueberry 2", 0x115bf4);

            AddFlat("Tortilla", 0xE4DABF);
            AddFlat("Tortilla Spots", 0xD59D62);
            AddFlat("Toasted Tortilla", 0xBF9C70);
            AddFlat("Toasted Tortilla Spots", 0xAC834E);

            AddFlat("Spinach", 0x4D7E33);
            AddFlat("Spinach Stem", 0x7C9C4F);

            AddFlat("Avocado", 0x364C2E);
            AddFlat("Avocado Inside", 0xBCC067);

            AddFlat("Strawberry", 0xCC2E36);
            AddFlat("Strawberry Inside", 0xF7C3A8);

            Log("Loaded materials.");
        }

        private void AddFlat(string name, int color)
        {
            AddMaterial(CreateFlat(name, color));
            AddMaterial(CreateFlat($"IngredientLib - \"{name}\"", color));
        }

        private void AddTransparent(string name, int color, float transparency)
        {
            AddMaterial(CreateTransparent(name, color, transparency));
            AddMaterial(CreateTransparent($"IngredientLib - \"{name}\"", color, transparency));
        }

        private void AddIcons()
        {
            Bundle.LoadAllAssets<Texture2D>();
            Bundle.LoadAllAssets<Sprite>();

            var icons = Bundle.LoadAsset<TMP_SpriteAsset>("Icon Asset");
            TMP_Settings.defaultSpriteAsset.fallbackSpriteAssets.Add(icons);
            icons.material = UnityEngine.Object.Instantiate(TMP_Settings.defaultSpriteAsset.material);
            icons.material.mainTexture = Bundle.LoadAsset<Texture2D>("Icon Texture");
        }
        #endregion

        #region Tweaks
        private void PerformTweak(GameData gameData)
        {
            if (ApplyRedirects.Value)
                RedirectGDOs(gameData);

            TweakBasegame();
        }

        private void RedirectGDOs(GameData gameData)
        {
            // Items
            foreach (var item in gameData.Get<Item>())
            {
                for (int i = 0; i < item.DerivedProcesses.Count; i++)
                {
                    var process = item.DerivedProcesses[i];
                    process.Result.TryRedirect(n => process.Result = n, gameData);
                    process.Process.TryRedirect(n => process.Process = n, gameData);
                }
                item.AutomaticItemProcess.Result.TryRedirect(n => item.AutomaticItemProcess.Result = n, gameData);
                item.AutomaticItemProcess.Process.TryRedirect(n => item.AutomaticItemProcess.Process = n, gameData);

                item.DirtiesTo.TryRedirect(n => item.DirtiesTo = n, gameData);
                item.DisposesTo.TryRedirect(n => item.DisposesTo = n, gameData);
                item.SplitSubItem.TryRedirect(n => item.SplitSubItem = n, gameData);
                item.DedicatedProvider.TryRedirect(n => item.DedicatedProvider = n, gameData);

                if (!(item is ItemGroup itemGroup))
                    continue;

                for (int i = 0; i < itemGroup.DerivedSets.Count; i++)
                {
                    var set = itemGroup.DerivedSets[i];
                    for (int i2 = 0; i2 < set.Items.Count; i2++)
                    {
                        set.Items[i2].TryRedirect(n => set.Items[i2] = n, gameData);
                    }
                }

                var prefab = itemGroup.Prefab;
                if (prefab == null || !prefab.TryGetComponent<ItemGroupView>(out var view))
                    continue;

                if (!view.ComponentGroups.IsNullOrEmpty())
                {
                    var newGroups = new List<ItemGroupView.ComponentGroup>();
                    for (int i = 0; i < view.ComponentGroups.Count; i++)
                    {
                        var group = view.ComponentGroups[i];
                        var groupItem = group.Item;
                        group.Item.TryRedirect(n => groupItem = n, gameData);
                        var newGroup = new ItemGroupView.ComponentGroup
                        {
                            GameObject = group.GameObject,
                            DrawAll = group.DrawAll,
                            IsDrawing = group.IsDrawing,
                            Item = groupItem,
                            Objects = group.Objects
                        };
                        newGroups.Add(newGroup);
                    }
                    view.ComponentGroups = newGroups;
                }

                var labelRefl = ReflectionUtils.GetField<ItemGroupView>("ComponentLabels");

                var labels = (List<ItemGroupView.ColourBlindLabel>)labelRefl.GetValue(view);
                if (!labels.IsNullOrEmpty())
                {
                    var newLabels = new List<ItemGroupView.ColourBlindLabel>();
                    for (int i = 0; i < labels.Count; i++)
                    {
                        var label = labels[i];
                        var labelItem = label.Item;
                        label.Item.TryRedirect(n => labelItem = n, gameData);
                        var newLabel = new ItemGroupView.ColourBlindLabel
                        {
                            Item = labelItem,
                            Text = label.Text
                        };
                        newLabels.Add(newLabel);
                    }
                    labelRefl.SetValue(view, newLabels);
                }
            }

            // Dishes
            foreach (var dish in gameData.Get<Dish>())
            {
                var minimumList = dish.MinimumIngredients.ToList();
                for (int i = 0; i < minimumList.Count; i++)
                {
                    minimumList[i].TryRedirect(n => minimumList[i] = n, gameData);
                }
                dish.MinimumIngredients = minimumList.ToHashSet();

                var processesList = dish.RequiredProcesses.ToList();
                for (int i = 0; i < processesList.Count; i++)
                {
                    processesList[i].TryRedirect(n => processesList[i] = n, gameData);
                }
                dish.RequiredProcesses = processesList.ToHashSet();
            }
        }

        private void TweakBasegame()
        {
            GetGDO<Item>(ItemReferences.Sugar).AddRecipe(GetCastedGDO<Item, Caramel>(), ProcessReferences.Cook, 2.6f, false, false);
            GetGDO<Item>(1069000119).AddRecipe(GetCastedGDO<Item, ChocolateShavings>(), ProcessReferences.Chop, 1f, false, false);
            GetGDO<Item>(ItemReferences.Water).AddRecipe(GetCastedGDO<Item, BoiledWater>(), ProcessReferences.Cook, 0f, false, true);

            UpdateCondiment<KetchupIngredient>(GetGDO<Item>(ItemReferences.CondimentKetchup));
            UpdateCondiment<MustardIngredient>(GetGDO<Item>(ItemReferences.CondimentMustard));
            UpdateCondiment<SoySauceIngredient>(GetGDO<Item>(ItemReferences.CondimentSoySauce));
        }

        private void UpdateCondiment<T>(Item condiment) where T : CustomItem
        {
            // Update condiment gdo
            condiment.SplitSubItem = GetCastedGDO<Item, T>();
            condiment.SplitCount = 999;
            condiment.PreventExplicitSplit = true;
            condiment.AllowSplitMerging = true;

            // Update provider gdo
            var provider = condiment.DedicatedProvider;
            provider.Properties = new()
            {
                GetCItemProvider(condiment.ID, 3, 3, false, false, false, false, false, true, false),
                new CItemHolder()
            };

            // Update provider prefab
            var prefab = provider.Prefab;
            prefab.transform.Find("HoldPoint").localPosition = prefab.GetChild(4).transform.localPosition;
        }
        #endregion

        #region Preferences
        public static PreferenceManager PreferenceManager;

        public static PreferenceBool ApplyRedirects;

        private void SetupMenu()
        {
            PreferenceManager = new(GUID);

            ApplyRedirects = PreferenceManager.RegisterPreference(new PreferenceBool("ApplyRedirects", true));
            PreferenceManager.Load();

            ModsPreferencesMenu<MainMenuAction>.RegisterMenu("IngredientLib", typeof(PreferencesMenu<MainMenuAction>), typeof(MainMenuAction));
            ModsPreferencesMenu<PauseMenuAction>.RegisterMenu("IngredientLib", typeof(PreferencesMenu<PauseMenuAction>), typeof(PauseMenuAction));
            Events.PreferenceMenu_MainMenu_CreateSubmenusEvent += (s, args) =>
            {
                args.Menus.Add(typeof(PreferencesMenu<MainMenuAction>), new PreferencesMenu<MainMenuAction>(args.Container, args.Module_list));
            };
            Events.PreferenceMenu_PauseMenu_CreateSubmenusEvent += (s, args) =>
            {
                args.Menus.Add(typeof(PreferencesMenu<PauseMenuAction>), new PreferencesMenu<PauseMenuAction>(args.Container, args.Module_list));
            };
        }
        #endregion

        protected override void OnInitialise()
        {
            LogWarning($"{GUID} v{VERSION} is in use!");
            RegisterMenu<ComponentMenu>();
        }

        protected override void OnPostActivate(Mod mod)
        {
            Bundle = mod.GetPacks<AssetBundleModPack>().SelectMany(e => e.AssetBundles).ToList()[0];

            AddGameData();

            AddMaterials();

            AddIcons();

            Events.BuildGameDataEvent += (s, args) =>
            {
                SetupMenu();

                PerformTweak(args.gamedata);

                AddLocalisations(args.gamedata);

                FixServedItems(args.gamedata);

                #region Debug logging
#if DEBUG
                Log("Custom References");
                Log("Providers");
                foreach (var item in providerReferences)
                    Debug.Log($" * \"{item.Key}\": `{item.Value}`");
                Log("Ingredient\n");
                foreach (var item in ingredientReferences)
                    Debug.Log($" * \"{item.Key}\": `{item.Value}`");
                Log("Split Ingredient\n");
                foreach (var item in splitIngredientReferences)
                    Debug.Log($" * \"{item.Key}\": `{item.Value}`");
                //
                Log("KitchenLib References");
                Log("Providers");
                foreach (var item in providerReferences)
                    Debug.Log($" * \"{GetCustomGameDataObject(item.Value).UniqueNameID}\": `{item.Value}`");
                Log("Ingredient\n");
                foreach (var item in ingredientReferences)
                    Debug.Log($" * \"{GetCustomGameDataObject(item.Value).UniqueNameID}\": `{item.Value}`");
                Log("Split Ingredient\n");
                foreach (var item in splitIngredientReferences)
                    Debug.Log($" * \"{GetCustomGameDataObject(item.Value).UniqueNameID}\": `{item.Value}`");
#endif
                #endregion

                args.gamedata.ProcessesView.Initialise(args.gamedata);
            };
        }

        #region Fixes
        internal void FixServedItems(GameData gamedata)
        {
            foreach (var gdoPair in gamedata.Objects)
            {
                var gdo = gdoPair.Value;

                if (!(gdo is Item item))
                    continue;

                if (item.NeedsIngredients == null) item.NeedsIngredients = new();
                if (item.SatisfiedBy == null) item.SatisfiedBy = new();
            }
        }
        #endregion

        #region Snapshot
        private string GetOrCreateFolder(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }
        private void SaveSnapshot(Texture2D snapshot, string folderPath, string fileName)
        {
            var path = Path.Combine(folderPath, fileName + ".png");
            if (!File.Exists(path))
                File.WriteAllBytes(path, snapshot.EncodeToPNG());
        }
        #endregion

        internal void AddGameData()
        {
            MethodInfo AddGDOMethod = typeof(BaseMod).GetMethod(nameof(BaseMod.AddGameDataObject));
            int counter = 0;
            Log("Registering GameDataObjects.");
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsAbstract || typeof(IWontRegister).IsAssignableFrom(type))
                    continue;

                if (!typeof(CustomGameDataObject).IsAssignableFrom(type))
                    continue;

                MethodInfo generic = AddGDOMethod.MakeGenericMethod(type);
                generic.Invoke(this, null);
                counter++;
            }
            Log($"Registered {counter} GameDataObjects.");
        }

        #region Localisation
        public static readonly Dictionary<string, string> GlobalLocalisationTexts = new()
        {
            { "IL:Repair", "Repair Save" }
        };

        public static readonly Dictionary<StartDayWarning, GenericLocalisationStruct> StartDayWarningLocalisationTexts = new()
        {
            { CorruptedSaveWarning, new()
                {
                    Name = "Corrupted save",
                    Description = "This can happen because of mod updates or other misc reasons.\nA repair button is infront of the restaurant or in the pause menu."
                }
            }
        };

        private void AddLocalisations(GameData gameData)
        {
            var baseTexts = gameData.GlobalLocalisation.Text;
            foreach (var text in GlobalLocalisationTexts)
            {
                if (baseTexts.ContainsKey(text.Key))
                    continue;
                baseTexts.Add(text.Key, text.Value);
            }

            var startDayTexts = gameData.GlobalLocalisation.StartDayWarningLocalisation.Text;
            foreach (var text in StartDayWarningLocalisationTexts)
            {
                if (startDayTexts.ContainsKey(text.Key))
                    continue;
                startDayTexts.Add(text.Key, text.Value);
            }
        }
        #endregion

        #region Logging
        internal static void LogInfo(string msg) { Debug.Log($"[{GUID}] " + msg); }
        internal static void LogWarning(string msg) { Debug.LogWarning($"[{GUID}] " + msg); }
        internal static void LogError(string msg) { Debug.LogError($"[{GUID}] " + msg); }
        internal static void LogInfo(object msg) { LogInfo($"[{GUID}] " + msg.ToString()); }
        internal static void LogWarning(object msg) { LogWarning($"[{GUID}] " + msg.ToString()); }
        internal static void LogError(object msg) { LogError($"[{GUID}] " + msg.ToString()); }
        #endregion
    }

    internal struct IWontRegister { }
}
