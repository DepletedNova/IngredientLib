namespace IngredientLib.Ingredient.Items
{
    public class TortillaChips : GenericItem<TortillaProvider>
    {
        public override string NameTag => "Tortilla Chips";

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 10f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                IsBad = true,
                Result = GetGDO<Item>(ItemReferences.BurnedFood)
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("tortilla", "Toasted Tortilla", "Toasted Tortilla Spots");
        }
    }
}
