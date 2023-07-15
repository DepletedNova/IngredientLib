namespace IngredientLib.Ingredient.Items
{
    public class SweetDoughMix : GenericItemGroup<SweetDoughMix.View>
    {
        public override string NameTag => "Sweet Dough Mix";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override List<ItemSet> Sets => new List<ItemSet>()
            {
                new ItemSet()
                {
                    Items = new List<Item>()
                    {
                        GetGDO<Item>(ItemReferences.Sugar),
                        GetGDO<Item>(ItemReferences.EggCracked),
                        GetGDO<Item>(ItemReferences.Dough),
                    },
                    Min = 3,
                    Max = 3
                }
            };

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Result = GetCastedGDO<Item, SweetDough>(),
                Process = GetGDO<Process>(ProcessReferences.Knead),
                Duration = 1.3f
            }
        };

        public override string ColourBlindTag => "SD";

        public override void Modify(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("Bowl", "Metal");

            Prefab.ApplyMaterialToChild("Sugar", "Sugar");
            Prefab.ApplyMaterialToChild("Egg", "Egg - White", "Egg - Yolk");
            Prefab.ApplyMaterialToChild("Dough", "Raw Pastry");
        }

        public class View : AccessedItemGroupView
        {
            protected override List<ComponentGroup> groups => new()
            {
                new()
                {
                    Item = GetGDO<Item>(ItemReferences.Sugar),
                    GameObject = gameObject.GetChild("Sugar")
                },
                new()
                {
                    Item = GetGDO<Item>(ItemReferences.EggCracked),
                    GameObject = gameObject.GetChild("Egg")
                },
                new()
                {
                    Item = GetGDO<Item>(ItemReferences.Dough),
                    GameObject = gameObject.GetChild("Dough")
                }
            };
        }
    }
}
