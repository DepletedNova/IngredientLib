namespace IngredientLib.Ingredient.Items
{
    public class PeeledGarlic : GenericItem<GarlicProvider>
    {
        public override string NameTag => "Peeled Garlic";
        public override List<Item.ItemProcess> Processes => new()
        {
            new Item.ItemProcess()
            {
                Duration = .6f,
                Process = GetGDO<Process>(ProcessReferences.Chop),
                Result = GetCastedGDO<Item, MincedGarlic>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("garlic", "Garlic");
        }
    }
}
