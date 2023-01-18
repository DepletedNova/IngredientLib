namespace IngredientLib.Ingredient.Items
{
    public class UnmixedEggDough : GenericItemGroup
    {
        public override string NameTag => "Unmixed Egg Dough";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override List<ItemSet> ItemSets => new List<ItemSet>()
            {
                new ItemSet()
                {
                    Items = new List<Item>()
                    {
                        GetGDO<Item>(ItemReferences.Flour),
                        GetGDO<Item>(ItemReferences.EggCracked),
                    },
                    Min = 2,
                    Max = 2
                }
            };

        public override void Modify(ItemGroup gdo)
        {
            var bowl = Prefab.GetChild("bowl");
            bowl.ApplyMaterialToChild("Cylinder.001", "Metal Dark");
            bowl.ApplyMaterialToChild("Cylinder", "Flour", "Egg - Yolk", "Egg - White");
        }
    }
}
