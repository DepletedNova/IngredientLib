namespace IngredientLib.Ingredient.Items
{
    public class Jalapeno : GenericItem<JalapenoProvider>
    {
        public override string NameTag => "Jalapeños";

        public override string ColourBlindTag => "Ja";

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 1.3f,
                Process = GetGDO<Process>(ProcessReferences.Chop),
                Result = GetCastedGDO<Item, ChoppedJalapeno>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("jalapeno", "Jalapeno", "Salad Leaf");
        }
    }
}
