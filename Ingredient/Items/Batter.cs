namespace IngredientLib.Ingredient.Items
{
    public class Batter : GenericItemGroup
    {
        public override string NameTag => "Batter";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override List<ItemSet> Sets => new List<ItemSet>()
            {
                new ItemSet()
                {
                    Items = new List<Item>()
                    {
                        GetGDO<Item>(ItemReferences.Flour),
                        GetCastedGDO<Item, MilkIngredient>(),
                    },
                    Min = 2,
                    Max = 2
                }
            };

        public override string ColourBlindTag => "Ba";

        public override void Modify(ItemGroup gdo)
        {
            var bowl = Prefab.GetChild("bowl");
            bowl.ApplyMaterialToChild("Cylinder.001", "Metal Dark");
            bowl.ApplyMaterialToChild("Cylinder", "Raw Pastry", "Flour");
        }
    }
}
