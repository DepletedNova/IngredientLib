namespace IngredientLib.Ingredient.Items
{
    public class Tortilla : GenericItem<TortillaProvider>
    {
        public override string NameTag => "Tortilla";

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 1.3f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Result = GetCastedGDO<Item, ToastedTortilla>()
            },
            new()
            {
                Duration = 1.3f,
                Process = GetGDO<Process>(ProcessReferences.Chop),
                Result = GetCastedGDO<Item, UncookedTortillaChips>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("tortilla", "Tortilla", "Tortilla Spots");
        }
    }
}
