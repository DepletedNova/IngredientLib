namespace IngredientLib.Ingredient.Items
{
    public class Spinach : GenericItem<SpinachProvider>
    {
        public override string NameTag => "Spinach";

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 0.4f,
                Process = GetGDO<Process>(ProcessReferences.Chop),
                Result = GetCastedGDO<Item, ChoppedSpinach>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("spinch", "Spinach", "Spinach Stem");
            Prefab.ApplyMaterialToChild("band", "Plastic - Red");
        }
    }
}
