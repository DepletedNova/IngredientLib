global using UnityEngine;
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
global using IngredientLib.ApplianceUtil;
global using IngredientLib.ApplianceUtil.Interactions;
global using IngredientLib.ApplianceUtil.Properties;
global using IngredientLib.ApplianceUtil.Views;
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
            // Providers
            AddGameDataObject<VinegarProvider>();
            AddGameDataObject<ChocolateProvider>();
            AddGameDataObject<ButterProvider>();
            AddGameDataObject<MilkProvider>();
            AddGameDataObject<BananaProvider>();
            AddGameDataObject<PepperProvider>();
            AddGameDataObject<LemonProvider>();
            AddGameDataObject<LimeProvider>();
            AddGameDataObject<OatsProvider>();
            AddGameDataObject<PorkProvider>();
            AddGameDataObject<ChickenProvider>();
            AddGameDataObject<DrumstickProvider>();
            AddGameDataObject<HoneyProvider>();
            AddGameDataObject<WhippingCreamProvider>();
            AddGameDataObject<IceProvider>();
#if DEBUG
            AddGameDataObject<TestProvider>();
#endif

            // Burned Foods
            AddGameDataObject<BurnedPorkchop>();

            // Ingredients
            AddGameDataObject<Vinegar>();
            AddGameDataObject<VinegarIngredient>();

            AddGameDataObject<WhippingCream>();
            AddGameDataObject<WhippingCreamIngredient>();

            AddGameDataObject<Chocolate>();
            AddGameDataObject<ChoppedChocolate>();
            AddGameDataObject<ChocolateShavings>();
            AddGameDataObject<ChocolateSauce>();
            AddGameDataObject<Ganache>();

            AddGameDataObject<ButterBlock>();
            AddGameDataObject<Butter>();

            AddGameDataObject<Milk>();
            AddGameDataObject<MilkIngredient>();

            AddGameDataObject<ChoppedBanana>();
            AddGameDataObject<BananaPeel>();
            AddGameDataObject<PeeledBanana>();
            AddGameDataObject<Banana>();

            AddGameDataObject<ChoppedPepper>();
            AddGameDataObject<Pepper>();

            AddGameDataObject<Lemon>();
            AddGameDataObject<ChoppedLemon>();
            AddGameDataObject<LemonJuice>();

            AddGameDataObject<Lime>();
            AddGameDataObject<ChoppedLime>();
            AddGameDataObject<LimeJuice>();

            AddGameDataObject<Oats>();

            AddGameDataObject<Pork>();
            AddGameDataObject<Porkchop>();
            AddGameDataObject<ChoppedPork>();
            AddGameDataObject<Bacon>();

            AddGameDataObject<ShreddedChicken>();
            AddGameDataObject<CookedChicken>();
            AddGameDataObject<Chicken>();

            AddGameDataObject<Drumstick>();
            AddGameDataObject<CookedDrumstick>();
            AddGameDataObject<BonelessDrumstick>();
            AddGameDataObject<CookedBonelessDrumstick>();
            AddGameDataObject<DrumstickBone>();

            AddGameDataObject<Honey>();
            AddGameDataObject<HoneyIngredient>();

            AddGameDataObject<UnmixedEggDough>();
            AddGameDataObject<EggDough>();

            AddGameDataObject<Caramel>();

            AddGameDataObject<Batter>();

            AddGameDataObject<Ice>();

            Log("Loaded ingredients.");
        }

        private void AddMaterials()
        {
            // Common
            AddMaterial(MaterialHelper.CreateTransparent("Glass", 0xF6FEFF, 0.6f));

            AddMaterial(MaterialHelper.CreateFlat("Stem", 0x6B3E26));
            AddMaterial(MaterialHelper.CreateFlat("White Fruit", 0xE5FFDC));

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

                args.gamedata.ProcessesView.Initialise(args.gamedata);
            };
        }
    }
}
