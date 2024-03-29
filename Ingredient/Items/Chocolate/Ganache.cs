﻿namespace IngredientLib.Ingredient.Items
{
    public class Ganache : GenericItemGroup<ChocolateProvider, ItemGroupView>
    {
        public override string NameTag => "Ganache";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override List<ItemSet> Sets => new List<ItemSet>()
            {
                new ItemSet()
                {
                    Items = new List<Item>()
                    {
                        GetCastedGDO<Item, WhippingCreamIngredient>(),
                        GetCastedGDO<Item, ChocolateSauce>()
                    },
                    Min = 2,
                    Max = 2
                }
            };

        public override string ColourBlindTag => "Ga";

        public override void Modify(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("Bowl", "Wood");
            Prefab.ApplyMaterialToChild("Chocolate", "Chocolate", "Chocolate Light");
        }
    }
}
