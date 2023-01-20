namespace IngredientLib.Ingredient.Items
{
    public class UncookedNoodlePot : GenericItemGroup
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
    }
}
