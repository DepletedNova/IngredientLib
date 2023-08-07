namespace IngredientLib.Ingredient.Items
{
    public class Strawberry : GenericItem<StrawberryProvider>
    {
        public override string NameTag => "Strawberry";

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Result = GetCastedGDO<Item, ChoppedStrawberry>(),
                Duration = .2f,
                Process = GetGDO<Process>(ProcessReferences.Chop)
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("strawberry", "Strawberry");
        }
    }
}
