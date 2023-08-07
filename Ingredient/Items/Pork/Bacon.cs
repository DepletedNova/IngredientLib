namespace IngredientLib.Ingredient.Items
{
    public class Bacon : GenericItem<PorkProvider>
    {
        public override string NameTag => "Bacon";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 2.5f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                IsBad = true,
                Result = GetGDO<Item>(ItemReferences.BurnedFood)
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("bacon", "Bacon", "Bacon Fat");
        }
    }
}
