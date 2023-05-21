global using UnityEngine;
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
            AddMaterial(CreateTransparent("Glass", 0xF6FEFF, 0.3f));
            AddMaterial(CreateTransparent("Plastic - Transparent", 0xF6FEFF, 0.7f));

            AddMaterial(CreateFlat("Stem", 0x6B3E26));
            AddMaterial(CreateFlat("White Fruit", 0xE5FFDC));

            AddMaterial(CreateFlat("Emoji Yellow", 0xFAC036));
            AddMaterial(CreateFlat("Emoji Orange", 0xE48C15));

            // Specific
            AddMaterial(CreateFlat("Vinegar", 0xFFF0C6));

            AddMaterial(CreateFlat("Chocolate", 0x7B3F00));
            AddMaterial(CreateFlat("Chocolate Dark", 0x753B00));
            AddMaterial(CreateFlat("Chocolate Light", 0x854400));

            AddMaterial(CreateFlat("Butter", 0xFAF28D));

            AddMaterial(CreateFlat("Banana", 0xFFFBC9));
            AddMaterial(CreateFlat("Banana Peel", 0xFFE135));
            AddMaterial(CreateFlat("Banana Inner", 0xE5DE6E));

            AddMaterial(CreateFlat("Lemon", 0xFFF443));
            AddMaterial(CreateFlat("Lemon Inner", 0xCCBB34));
            AddMaterial(CreateFlat("Lemon Juice", 0xFFF26D));

            AddMaterial(CreateFlat("Lime", 0x549231));
            AddMaterial(CreateFlat("Lime Inner", 0x497109));
            AddMaterial(CreateFlat("Lime Juice", 0xBCE896));

            AddMaterial(CreateFlat("Oat Mound", 0xdfc496));
            AddMaterial(CreateFlat("Oat Grain", 0xd8c29d));

            AddMaterial(CreateFlat("Pork Fat", 0xFFCBCB));
            AddMaterial(CreateFlat("Pork", 0xE291AC));
            AddMaterial(CreateFlat("Porkchop Fat", 0xB38F5A));
            AddMaterial(CreateFlat("Porkchop", 0xEADCB3));
            AddMaterial(CreateFlat("Bacon Fat", 0xC5A099));
            AddMaterial(CreateFlat("Bacon", 0x8E3D2F));

            AddMaterial(CreateFlat("Raw Chicken", 0xFFBEBE));
            AddMaterial(CreateFlat("Cooked Chicken", 0xB95B24));

            AddMaterial(CreateFlat("Cooked Drumstick", 0xB65B28));
            AddMaterial(CreateFlat("Raw Drumstick Bone", 0xC9B9B4));
            AddMaterial(CreateFlat("Cooked Drumstick Bone", 0xC69067));

            AddMaterial(CreateFlat("Honey", 0xEBA937));

            AddMaterial(CreateFlat("Egg Dough", 0xF4E2AA));

            AddMaterial(CreateFlat("Caramel", 0xC68E17));

            AddMaterial(CreateFlat("Cinnamon", 0xC58C66));

            AddMaterial(CreateFlat("Garlic", 0xf2e9d2));

            AddMaterial(CreateFlat("Jalapeno", 0x548042));

            AddMaterial(CreateFlat("Blueberry", 0x4f86f7));
            AddMaterial(CreateFlat("Blueberry 2", 0x115bf4));

            AddMaterial(CreateFlat("Tortilla", 0xE4DABF));
            AddMaterial(CreateFlat("Tortilla Spots", 0xD59D62));
            AddMaterial(CreateFlat("Toasted Tortilla", 0xBF9C70));
            AddMaterial(CreateFlat("Toasted Tortilla Spots", 0xAC834E));

            AddMaterial(CreateFlat("Spinach", 0x4D7E33));
            AddMaterial(CreateFlat("Spinach Stem", 0x7C9C4F));

            AddMaterial(CreateFlat("Avocado", 0x4E7030));
            AddMaterial(CreateFlat("Avocado Inside", 0xBCC067));
            AddMaterial(CreateFlat("Avocado Mash", 0x89AA40));

            AddMaterial(CreateFlat("Strawberry", 0xCC2E36));
            AddMaterial(CreateFlat("Strawberry Inside", 0xF7C3A8));

            Log("Loaded materials.");
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
