namespace IngredientLib.Ingredient.Items
{
    public class UnmixedEggDough : GenericItemGroup, IWontRegister
    {
        public override string NameTag => "Unmixed Egg Dough";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override List<ItemSet> Sets => new List<ItemSet>()
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

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 1.6f,
                Process = GetGDO<Process>(ProcessReferences.Knead),
                Result = GetCastedGDO<Item, EggDough>()
            }
        };

        public override void Modify(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("Bowl", "Metal Dark");
            Prefab.ApplyMaterialToChild("Flour", "Flour");
            Prefab.ApplyMaterialToChild("Yolk", "Egg - Yolk");
            Prefab.ApplyMaterialToChild("Whites", "Egg - White");
        }
    }
}
