﻿global using UnityEngine;
global using UnityEngine.VFX;

global using System;
global using System.Linq;
global using System.Reflection;
global using System.Collections.Generic;

global using Kitchen;
global using Kitchen.Components;
global using KitchenData;
global using KitchenMods;

global using KitchenLib;
global using KitchenLib.Customs;
global using KitchenLib.Event;
global using KitchenLib.References;
global using KitchenLib.Utils;

global using IngredientLib.Ingredient.Items;
global using IngredientLib.Ingredient.Providers;
global using IngredientLib.Util.Custom;
global using IngredientLib.Util;

global using HarmonyLib;

global using static KitchenData.ItemGroup;
global using static KitchenLib.Utils.GDOUtils;
global using static KitchenLib.Utils.KitchenPropertiesUtils;
global using static KitchenLib.Utils.MaterialUtils;
global using static KitchenLib.Utils.GameObjectUtils;
global using static IngredientLib.References;
global using static IngredientLib.Util.Helper;

namespace IngredientLib
{
    public class Main : BaseMod
    {
        public const string GUID = "ingredientlib";
        public const string VERSION = "0.5.0";

        public Main() : base(GUID, "IngredientLib", "Depleted Supernova#1957", VERSION, ">=1.1.0", Assembly.GetExecutingAssembly()) { }

        public static AssetBundle bundle;

        private void AddMaterials()
        {
            // Common
            AddTransparent("Glass", 0xF6FEFF, 0.3f);
            AddTransparent("Plastic - Transparent", 0xF6FEFF, 0.7f);

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

            AddFlat("Lemon", 0xFFF44);
            AddFlat("Lemon Inner", 0xCCBB34);
            AddFlat("Lemon Juice", 0xFFF26D);

            AddFlat("Lime", 0x549231);
            AddFlat("Lime Inner", 0x497109);
            AddFlat("Lime Juice", 0xBCE896);

            AddFlat("Oat Mound", 0xdfc496);
            AddFlat("Oat Grain", 0xd8c29d);

            AddFlat("Pork Fat", 0xFFCBCB);
            AddFlat("Pork", 0xE291AC);
            AddFlat("Porkchop Fat", 0xB38F5A);
            AddFlat("Porkchop", 0xEADCB3);
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

            AddFlat("Avocado", 0x4E7030);
            AddFlat("Avocado Inside", 0xBCC067);
            AddFlat("Avocado Mash", 0x89AA40);

            AddFlat("Strawberry", 0xCC2E36);
            AddFlat("Strawberry Inside", 0xF7C3A8);

            Log("Loaded materials.");
        }

        // IMPORTANT - I do not recommend the "IngredientLib - " format in your mods. I want to remove it eventually.
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

        protected override void OnPostActivate(Mod mod)
        {
            bundle = mod.GetPacks<AssetBundleModPack>().SelectMany(e => e.AssetBundles).ToList()[0];

            AddGameData();

            AddMaterials();

            Events.BuildGameDataEvent += (s, args) =>
            {
                GetGDO<Item>(ItemReferences.Sugar).AddRecipe(GetCastedGDO<Item, Caramel>(), ProcessReferences.Cook, 2.6f, false, false);

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

                args.gamedata.ProcessesView.Initialise(args.gamedata);
            };
        }

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
    }

    internal struct IWontRegister { }
}
