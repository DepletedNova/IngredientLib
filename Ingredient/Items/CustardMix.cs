namespace IngredientLib.Ingredient.Items
{
    public class CustardMix : GenericItemGroup<CustardMix.View>
    {
        public override string NameTag => "Custard Mix";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override List<ItemSet> Sets => new List<ItemSet>()
            {
                new ItemSet()
                {
                    Items = new List<Item>()
                    {
                        GetGDO<Item>(ItemReferences.Sugar),
                        GetGDO<Item>(ItemReferences.EggCracked),
                        GetCastedGDO<Item, WhippingCreamIngredient>(),
                    },
                    Min = 3,
                    Max = 3
                }
            };

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Result = GetCastedGDO<Item, Custard>(),
                Process = GetGDO<Process>(ProcessReferences.Knead),
                Duration = 0.75f
            }
        };

        public override string ColourBlindTag => "Cu";

        public override void Modify(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("Bowl", "Metal- Shiny");

            Prefab.ApplyMaterialToChild("Sugar", "Sugar");
            Prefab.ApplyMaterialToChild("Egg", "Egg - White", "Egg - Yolk");
            Prefab.ApplyMaterialToChild("Cream", "Coffee Cup");
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
                    Item = GetCastedGDO<Item, WhippingCreamIngredient>(),
                    GameObject = gameObject.GetChild("Cream")
                }
            };
        }
    }
}
