namespace IngredientLib.Ingredient.Items
{
    public class UncookedMacaroniPot : GenericItemGroup
    {
        public override string NameTag => "Potted Macaroni";
        public override GameObject Prefab => GetPrefab("Potted Pasta");
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
    }
}
