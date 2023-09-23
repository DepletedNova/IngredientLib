namespace IngredientLib.Ingredient.Items
{
    public class ThinBatter : GenericItemGroup<ThinBatter.View>, IWontRegister
    {
        public override string NameTag => "Thin Batter";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override List<ItemSet> Sets => new List<ItemSet>()
            {
                new ItemSet()
                {
                    Items = new List<Item>()
                    {
                        GetGDO<Item>(ItemReferences.Flour),
                        GetGDO<Item>(ItemReferences.EggCracked),
                        GetGDO<Item>(329108931),
                    },
                    Min = 3,
                    Max = 3
                }
            };

        public override string ColourBlindTag => "TBa";

        public override void Modify(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("Bowl", "Metal- Shiny Blue");

            Prefab.ApplyMaterialToChild("Flour", "Flour");
            Prefab.ApplyMaterialToChild("Egg", "Egg - White", "Egg - Yolk");
            Prefab.ApplyMaterialToChild("Milk", "Milk");
        }

        public class View : AccessedItemGroupView
        {
            protected override List<ComponentGroup> groups => new()
            {
                new()
                {
                    Item = GetGDO<Item>(ItemReferences.Flour),
                    GameObject = gameObject.GetChild("Flour")
                },
                new()
                {
                    Item = GetGDO<Item>(ItemReferences.EggCracked),
                    GameObject = gameObject.GetChild("Egg")
                },
                new()
                {
                    Item = GetGDO<Item>(329108931),
                    GameObject = gameObject.GetChild("Milk")
                }
            };
        }
    }
}
