namespace IngredientLib.Ingredient.Items
{
    public class UncookedNoodlePot : GenericItemGroup<UncookedNoodlePot.ItemGroupViewAccessed>
    {
        public override string NameTag => "Potted Pasta";
        public override GameObject Prefab => GetPrefab("Potted Pasta");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override Item DisposesTo => GetGDO<Item>(ItemReferences.Pot);
        public override List<ItemSet> Sets => new()
        {
            new ItemSet()
            {
                IsMandatory = false,
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
                IsMandatory = false,
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
        public override List<Item.ItemProcess> Processes => new()
        {
            new Item.ItemProcess()
            {
                Duration = 4.5f,
                Result = GetCastedGDO<Item, CookedNoodlePot>(),
                Process = GetGDO<Process>(ProcessReferences.Cook),
            }
        };
        public override void Modify(ItemGroup gdo)
        {
            Prefab.GetComponent<ItemGroupViewAccessed>().Setup(gdo);
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
                    GameObject = gameObject.GetChild("Pasta").GetChild("BoxNoodle"),
                    Item = GetCastedGDO<Item, BoxNoodle>()
                },
                new ComponentGroup()
                {
                    GameObject = gameObject.GetChild("Pasta").GetChild("EggNoodle"),
                    Item = GetCastedGDO<Item, EggNoodle>()
                },
                new ComponentGroup()
                {
                    GameObject = gameObject.GetChild("Macaroni").GetChild("Mac"),
                    Item = GetCastedGDO<Item, Macaroni>()
                },
                new ComponentGroup()
                {
                    Objects = new()
                    {
                        gameObject.GetChild("Pasta").GetChild("CookedNoodle"),
                        gameObject.GetChild("Steam")
                    },
                    DrawAll = true,
                    Item = GetCastedGDO<Item, CookedNoodlePot>()
                },
                new ComponentGroup()
                {
                    Objects = new()
                    {
                        gameObject.GetChild("Macaroni").GetChild("CookedMac"),
                        gameObject.GetChild("Steam")
                    },
                    DrawAll = true,
                    Item = GetCastedGDO<Item, CookedMacaroniPot>()
                }
            };

            protected override List<ColourBlindLabel> labels => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, Macaroni>(),
                    Text = "Ma"
                },
                new()
                {
                    Item = GetCastedGDO<Item, BoxNoodle>(),
                    Text = "No"
                },
                new()
                {
                    Item = GetCastedGDO<Item, EggNoodle>(),
                    Text = "No"
                }
            };
        }
    }
}
