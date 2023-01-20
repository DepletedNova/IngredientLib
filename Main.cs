global using UnityEngine;
global using UnityEngine.VFX;
global using Unity.Entities;
global using Unity.Collections;
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
global using IngredientLib.Util;
global using IngredientLib.Util.Custom;
global using IngredientLib.Util.Interactions;
global using IngredientLib.Util.Properties;
global using IngredientLib.Util.Views;
global using MessagePack;
global using HarmonyLib;

global using static KitchenData.ItemGroup;
global using static KitchenLib.Utils.GDOUtils;
global using static KitchenLib.Utils.KitchenPropertiesUtils;
global using static IngredientLib.Util.Helper;
global using static IngredientLib.References;

namespace IngredientLib
{
    public class Main : BaseMod
    {
        public const string GUID = "nova.ingredientlib";
        public const string VERSION = "0.2.2";

        public Main() : base(GUID, "IngredientLib", "Depleted Supernova#1957", VERSION, ">=1.0.0", Assembly.GetExecutingAssembly()) { }

        public static AssetBundle bundle;

        private void AddGameData()
        {
            // Vinegar
            AddGameDataObject<VinegarProvider>();
            AddGameDataObject<Vinegar>();
            AddGameDataObject<VinegarIngredient>();

            // Whipping Cream
            AddGameDataObject<WhippingCreamProvider>();
            AddGameDataObject<WhippingCream>();
            AddGameDataObject<WhippingCreamIngredient>();

            // Chocolate
            AddGameDataObject<ChocolateProvider>();
            AddGameDataObject<Chocolate>();
            AddGameDataObject<ChoppedChocolate>();
            AddGameDataObject<ChocolateShavings>();
            AddGameDataObject<ChocolateSauce>();
            AddGameDataObject<Ganache>();

            // Butter
            AddGameDataObject<ButterProvider>();
            AddGameDataObject<ButterBlock>();
            AddGameDataObject<Butter>();

            // Milk
            AddGameDataObject<MilkProvider>();
            AddGameDataObject<Milk>();
            AddGameDataObject<MilkIngredient>();

            // Banana
            AddGameDataObject<BananaProvider>();
            AddGameDataObject<Banana>();
            AddGameDataObject<PeeledBanana>();
            AddGameDataObject<ChoppedBanana>();
            AddGameDataObject<BananaPeel>();

            // Pepper
            AddGameDataObject<PepperProvider>();
            AddGameDataObject<Pepper>();
            AddGameDataObject<ChoppedPepper>();

            // Lemon
            AddGameDataObject<LemonProvider>();
            AddGameDataObject<Lemon>();
            AddGameDataObject<ChoppedLemon>();
            AddGameDataObject<LemonJuice>();

            // Lime
            AddGameDataObject<LimeProvider>();
            AddGameDataObject<Lime>();
            AddGameDataObject<ChoppedLime>();
            AddGameDataObject<LimeJuice>();

            // Oats
            AddGameDataObject<OatsProvider>();
            AddGameDataObject<Oats>();

            // Pork
            AddGameDataObject<PorkProvider>();
            AddGameDataObject<Pork>();
            AddGameDataObject<Porkchop>();
            AddGameDataObject<ChoppedPork>();
            AddGameDataObject<Bacon>();
            AddGameDataObject<BurnedPorkchop>();

            // Chicken
            AddGameDataObject<ChickenProvider>();
            AddGameDataObject<Chicken>();
            AddGameDataObject<ShreddedChicken>();
            AddGameDataObject<CookedChicken>();

            // Drumstick
            AddGameDataObject<DrumstickProvider>();
            AddGameDataObject<Drumstick>();
            AddGameDataObject<CookedDrumstick>();
            AddGameDataObject<BonelessDrumstick>();
            AddGameDataObject<CookedBonelessDrumstick>();
            AddGameDataObject<DrumstickBone>();

            // Honey
            AddGameDataObject<HoneyProvider>();
            AddGameDataObject<Honey>();
            AddGameDataObject<HoneyIngredient>();

            // Pasta
            AddGameDataObject<BoxPastaProvider>();
            AddGameDataObject<UnmixedEggDough>();
            AddGameDataObject<EggDough>();
            AddGameDataObject<EggNoodle>();
            AddGameDataObject<BoxNoodle>();
            AddGameDataObject<UncookedNoodlePot>();
            AddGameDataObject<CookedNoodlePot>();

            // Macaroni
            AddGameDataObject<MacaroniProvider>();
            AddGameDataObject<Macaroni>();
            AddGameDataObject<UncookedMacaroniPot>();
            AddGameDataObject<CookedMacaroniPot>();

            // Caramel
            AddGameDataObject<Caramel>();

            // Batter
            AddGameDataObject<Batter>();

            // Ice
            AddGameDataObject<IceProvider>();
            AddGameDataObject<Ice>();

            // Cinnamon
            AddGameDataObject<CinnamonProvider>();
            AddGameDataObject<Cinnamon>();

            // Syrup
            AddGameDataObject<SyrupProvider>();
            AddGameDataObject<Syrup>();
            AddGameDataObject<SyrupIngredient>();

            Log("Loaded ingredients.");
        }

        private void AddMaterials()
        {
            // Common
            AddMaterial(MaterialHelper.CreateTransparent("Glass", 0xF6FEFF, 0.6f));
            AddMaterial(MaterialHelper.CreateTransparent("Plastic - Transparent", 0xF6FEFF, 0.9f));

            AddMaterial(MaterialHelper.CreateFlat("Stem", 0x6B3E26));
            AddMaterial(MaterialHelper.CreateFlat("White Fruit", 0xE5FFDC));

            AddMaterial(MaterialHelper.CreateFlat("Emoji Yellow", 0xFAC036));
            AddMaterial(MaterialHelper.CreateFlat("Emoji Orange", 0xE48C15));

            // Specific
            AddMaterial(MaterialHelper.CreateFlat("Vinegar", 0xFFF0C6));

            AddMaterial(MaterialHelper.CreateFlat("Chocolate", 0x7B3F00));
            AddMaterial(MaterialHelper.CreateFlat("Chocolate Dark", 0x753B00));
            AddMaterial(MaterialHelper.CreateFlat("Chocolate Light", 0x854400));

            AddMaterial(MaterialHelper.CreateFlat("Butter", 0xFAF28D));

            AddMaterial(MaterialHelper.CreateFlat("Banana", 0xFFFBC9));
            AddMaterial(MaterialHelper.CreateFlat("Banana Peel", 0xFFE135));
            AddMaterial(MaterialHelper.CreateFlat("Banana Inner", 0xE5DE6E));

            AddMaterial(MaterialHelper.CreateFlat("Lemon", 0xFFF443));
            AddMaterial(MaterialHelper.CreateFlat("Lemon Inner", 0xCCBB34));
            AddMaterial(MaterialHelper.CreateFlat("Lemon Juice", 0xFFF26D));

            AddMaterial(MaterialHelper.CreateFlat("Lime", 0x549231));
            AddMaterial(MaterialHelper.CreateFlat("Lime Inner", 0x497109));
            AddMaterial(MaterialHelper.CreateFlat("Lime Juice", 0xBCE896));

            AddMaterial(MaterialHelper.CreateFlat("Oat Mound", 0xdfc496));
            AddMaterial(MaterialHelper.CreateFlat("Oat Grain", 0xd8c29d));

            AddMaterial(MaterialHelper.CreateFlat("Pork Fat", 0xFFCBCB));
            AddMaterial(MaterialHelper.CreateFlat("Pork", 0xE291AC));
            AddMaterial(MaterialHelper.CreateFlat("Porkchop Fat", 0xB38F5A));
            AddMaterial(MaterialHelper.CreateFlat("Porkchop", 0xEADCB3));
            AddMaterial(MaterialHelper.CreateFlat("Bacon Fat", 0xC5A099));
            AddMaterial(MaterialHelper.CreateFlat("Bacon", 0x8E3D2F));

            AddMaterial(MaterialHelper.CreateFlat("Raw Chicken", 0xFFBEBE));
            AddMaterial(MaterialHelper.CreateFlat("Cooked Chicken", 0xB95B24));

            AddMaterial(MaterialHelper.CreateFlat("Cooked Drumstick", 0xB65B28));
            AddMaterial(MaterialHelper.CreateFlat("Raw Drumstick Bone", 0xC9B9B4));
            AddMaterial(MaterialHelper.CreateFlat("Cooked Drumstick Bone", 0xC69067));

            AddMaterial(MaterialHelper.CreateFlat("Honey", 0xEBA937));

            AddMaterial(MaterialHelper.CreateFlat("Egg Dough", 0xF4E2AA));

            AddMaterial(MaterialHelper.CreateFlat("Caramel", 0xC68E17));

            AddMaterial(MaterialHelper.CreateFlat("Cinnamon", 0xC58C66));

            Log("Loaded materials.");
        }

        private void AddRecipes()
        {
            var burnedFood = GetGDO<Item>(ItemReferences.BurnedFood);

            GetItem<Chocolate>().AddRecipe(GetItem<ChoppedChocolate>(), ProcessReferences.Chop, 1f, false, false);
            GetItem<Chocolate>().AddRecipe(GetItem<ChocolateSauce>(), ProcessReferences.Cook, 2.4f, false, false);
            GetItem<ChoppedChocolate>().AddRecipe(GetItem<ChocolateShavings>(), ProcessReferences.Chop, 0.5f, false, false);
            GetItem<ChoppedChocolate>().AddRecipe(GetItem<ChocolateSauce>(), ProcessReferences.Cook, 0.6f, false, false);
            GetItem<ChocolateShavings>().AddRecipe(GetItem<ChocolateSauce>(), ProcessReferences.Cook, 0.4f, false, false);
            GetItem<ChocolateSauce>().AddRecipe(null, ProcessReferences.Cook, 15f, true, false);

            GetItem<PeeledBanana>().AddRecipe(GetItem<ChoppedBanana>(), ProcessReferences.Chop, 1.3f, false, false);

            GetItem<Pepper>().AddRecipe(GetItem<ChoppedPepper>(), ProcessReferences.Chop, 1.3f, false, false);

            GetItem<Lemon>().AddRecipe(GetItem<ChoppedLemon>(), ProcessReferences.Chop, 1.3f, false, false);
            GetItem<ChoppedLemon>().AddRecipe(GetItem<LemonJuice>(), ProcessReferences.Knead, 1.3f, false, false);

            GetItem<Lime>().AddRecipe(GetItem<ChoppedLime>(), ProcessReferences.Chop, 1.3f, false, false);
            GetItem<ChoppedLime>().AddRecipe(GetItem<LimeJuice>(), ProcessReferences.Knead, 1.3f, false, false);

            GetItem<Pork>().AddRecipe(GetItem<Porkchop>(), ProcessReferences.Cook, 6.5f, false, false);
            GetItem<Pork>().AddRecipe(GetItem<ChoppedPork>(), ProcessReferences.Chop, 2.4f, false, false);
            GetItem<Porkchop>().AddRecipe(GetItem<BurnedPorkchop>(), ProcessReferences.Cook, 10f, true, false);
            GetItem<ChoppedPork>().AddRecipe(GetItem<Bacon>(), ProcessReferences.Cook, 4f, false, false);
            GetItem<Bacon>().AddRecipe(burnedFood, ProcessReferences.Cook, 8f, true, false);

            GetItem<Chicken>().AddRecipe(GetItem<CookedChicken>(), ProcessReferences.Cook, 6f, false, false);
            GetItem<CookedChicken>().AddRecipe(burnedFood, ProcessReferences.Cook, 10f, true, false);
            GetItem<CookedChicken>().AddRecipe(GetItem<ShreddedChicken>(), ProcessReferences.Chop, 1.3f, false, false);

            GetItem<Drumstick>().AddRecipe(GetItem<CookedDrumstick>(), ProcessReferences.Cook, 6f, false, false);
            GetItem<CookedDrumstick>().AddRecipe(burnedFood, ProcessReferences.Cook, 10f, true, false);
            GetItem<BonelessDrumstick>().AddRecipe(GetItem<CookedBonelessDrumstick>(), ProcessReferences.Cook, 6f, false, false);
            GetItem<CookedBonelessDrumstick>().AddRecipe(burnedFood, ProcessReferences.Cook, 10f, true, false);

            GetItem<UnmixedEggDough>().AddRecipe(GetItem<EggDough>(), ProcessReferences.Knead, 1.6f, false, false);
            GetItem<EggDough>().AddRecipe(GetItem<EggNoodle>(), ProcessReferences.Knead, 1.3f, false, false);

            GetGDO<Item>(ItemReferences.Sugar).AddRecipe(GetItem<Caramel>(), ProcessReferences.Cook, 2.6f, false, false);
            GetItem<Caramel>().AddRecipe(burnedFood, ProcessReferences.Cook, 10f, true, false);

            Log("Loaded base recipes.");

        }

        private Item GetItem<T>() where T : CustomItem
        {
            return GetCustomGameDataObject<T>().GameDataObject as Item;
        }

        public override void PostActivate(Mod mod)
        {
            bundle = mod.GetPacks<AssetBundleModPack>().SelectMany(e => e.AssetBundles).ToList()[0];

            AddGameData();

            AddMaterials();

            Events.BuildGameDataEvent += (s, args) =>
            {
                AddRecipes();

#if DEBUG
                Log("Providers");
                foreach (var item in providerReferences)
                    Debug.Log($" * \"{item.Key}\": `{item.Value}`");
                Log("Ingredient\n");
                foreach (var item in ingredientReferences)
                    Debug.Log($" * \"{item.Key}\": `{item.Value}`");
                Log("Split Ingredient\n");
                foreach (var item in splitIngredientReferences)
                    Debug.Log($" * \"{item.Key}\": `{item.Value}`");
#endif

                args.gamedata.ProcessesView.Initialise(args.gamedata);
            };
        }
    }
}
