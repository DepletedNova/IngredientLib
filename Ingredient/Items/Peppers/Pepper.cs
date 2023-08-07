namespace IngredientLib.Ingredient.Items
{
    public class Pepper : GenericItem<PepperProvider>
    {
        public override string NameTag => "Peppers";

        public override string ColourBlindTag => "Pe";

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = .6f,
                Process = GetGDO<Process>(ProcessReferences.Chop),
                Result = GetCastedGDO<Item, ChoppedPepper>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("jalapeno", "Tomato", "Salad Leaf");
        }
    }
}
