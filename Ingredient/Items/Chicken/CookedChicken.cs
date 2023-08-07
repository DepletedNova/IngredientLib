namespace IngredientLib.Ingredient.Items
{
    public class CookedChicken : GenericItem<ChickenProvider>
    {
        public override string NameTag => "Cooked Chicken";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 10f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                IsBad = true,
                Result = GetGDO<Item>(ItemReferences.BurnedFood)
            },
            new()
            {
                Duration = .6f,
                Process = GetGDO<Process>(ProcessReferences.Chop),
                Result = GetCastedGDO<Item, ShreddedChicken>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("chicken", "Cooked Chicken");
        }
    }
}
