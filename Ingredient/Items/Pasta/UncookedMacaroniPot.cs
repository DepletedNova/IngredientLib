namespace IngredientLib.Ingredient.Items
{
    public class UncookedMacaroniPot : GenericItemGroup<UncookedMacaroniPot.ItemGroupViewAccessed>
    {
        public override string NameTag => "Potted Macaroni";
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override Item DisposesTo => GetGDO<Item>(ItemReferences.Pot);
        public override List<ItemSet> Sets => new()
        {
            new ItemSet()
            {
                IsMandatory = false,
                Max = 2,
                Min = 2,
                Items = new()
                {
                    GetCastedGDO<Item, Macaroni>(),
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

        public override List<Item.ItemProcess> Processes => new()
        {
            new Item.ItemProcess()
            {
                Duration = 4.5f,
                Result = GetCastedGDO<Item, CookedMacaroniPot>(),
                Process = GetGDO<Process>(ProcessReferences.Cook),
            }
        };

        public override void Modify(ItemGroup gdo)
        {
            Prefab.GetComponent<ItemGroupViewAccessed>().Setup(gdo);

            Prefab.ApplyMaterialToChild("Handles", "Metal Dark");
            Prefab.ApplyMaterialToChild("Pot", "Metal");
            Prefab.ApplyMaterialToChild("Water", "Water");
            Prefab.GetChild("Macaroni").ApplyMaterialToChildren("Mac", "Sack");
        }

        public class ItemGroupViewAccessed : AccessedItemGroupView
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
                    GameObject = gameObject.GetChild("Macaroni"),
                    Item = GetCastedGDO<Item, Macaroni>()
                }
            };
        }
    }
}
