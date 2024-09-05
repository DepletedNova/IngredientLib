namespace IngredientLib.Ingredient.Items
{
    public class UncookedNoodlePot : GenericItemGroup<UncookedNoodlePot.ItemGroupViewAccessed>
    {
        public override string NameTag => "Potted Pasta";
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override Item DisposesTo => GetGDO<Item>(ItemReferences.Pot);

        public override List<Item.ItemProcess> Processes => new()
        {
            new Item.ItemProcess()
            {
                Duration = 4.5f,
                Result = GetCastedGDO<Item, CookedNoodlePot>(),
                Process = GetGDO<Process>(ProcessReferences.Cook),
            }
        };

        public override List<ItemSet> Sets => new()
        {
            new ItemSet()
            {
                Max = 1,
                Min = 1,
                Items = new()
                {
                    GetCastedGDO<Item, EggNoodle>(),
                    GetCastedGDO<Item, BoxNoodle>()
                }
            },
            new ItemSet()
            {
                Max = 1,
                Min = 1,
                Items = new()
                {
                    GetGDO<Item>(ItemReferences.Water)
                }
            },
            new ItemSet()
            {
                IsMandatory = true,
                Max = 1,
                Min = 1,
                Items = new()
                {
                    GetGDO<Item>(ItemReferences.Pot)
                }
            }
        };

        public override void Modify(ItemGroup gdo)
        {
            Prefab.GetComponent<ItemGroupViewAccessed>().Setup(gdo);

            Prefab.ApplyMaterialToChild("Handles", "Metal Dark");
            Prefab.ApplyMaterialToChild("Pot", "Metal");
            Prefab.ApplyMaterialToChild("Water", "Water");
            Prefab.ApplyMaterialToChild("Un-Egg", "Egg Dough", "Sack");
            Prefab.GetChild("Un-Stick").ApplyMaterialToChildren("Noodle", "Sack");

            this.Redirect(ItemReferences.SpaghettiUncooked);
        }

        public class ItemGroupViewAccessed : GenericItemGroup.AccessedItemGroupView
        {
            protected override List<ComponentGroup> groups => new()
            {
                new()
                {
                    GameObject = gameObject.GetChild("Water"),
                    Item = GetGDO<Item>(ItemReferences.Water)
                },
                new ComponentGroup()
                {
                    GameObject = gameObject.GetChild("Un-Stick"),
                    Item = GetCastedGDO<Item, BoxNoodle>()
                },
                new ComponentGroup()
                {
                    GameObject = gameObject.GetChild("Un-Egg"),
                    Item = GetCastedGDO<Item, EggNoodle>()
                }
            };
        }
    }
}
