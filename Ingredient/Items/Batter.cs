namespace IngredientLib.Ingredient.Items
{
    public class Batter : GenericItemGroup<Batter.View>, IWontRegister
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
                        GetGDO<Item>(ItemReferences.EggCracked),
                        GetCastedGDO<Item, Butter>()
                    },
                    Min = 3,
                    Max = 3
                }
            };

        public override string ColourBlindTag => "Ba";

        public override void Modify(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("Bowl", "Metal - Brass");

            Prefab.ApplyMaterialToChild("Flour", "Flour");
            Prefab.ApplyMaterialToChild("Egg", "Egg - White", "Egg - Yolk");
            Prefab.ApplyMaterialToChild("Butter", "Butter");
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
                    Item = GetCastedGDO<Item, Butter>(),
                    GameObject = gameObject.GetChild("Butter")
                },
            };
        }
    }
}
