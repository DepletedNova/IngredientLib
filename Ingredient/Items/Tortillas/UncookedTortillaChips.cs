namespace IngredientLib.Ingredient.Items
{
    public class UncookedTortillaChips : GenericItem<TortillaProvider>
    {
        public override string NameTag => "Unfried Tortilla Chips";

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 3.2f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Result = GetCastedGDO<Item, TortillaChips>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("tortilla", "Tortilla", "Tortilla Spots");
        }
    }
}
