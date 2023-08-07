namespace IngredientLib.Ingredient.Items
{
    public class Garlic : GenericItem<GarlicProvider>
    {
        public override string NameTag => "Garlic";
        public override List<Item.ItemProcess> Processes => new()
        {
            new Item.ItemProcess()
            {
                Duration = 1f,
                Process = GetGDO<Process>(ProcessReferences.Chop),
                Result = GetCastedGDO<Item, PeeledGarlic>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("garlic", "Garlic");
        }
    }
}
