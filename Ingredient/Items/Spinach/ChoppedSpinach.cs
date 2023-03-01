namespace IngredientLib.Ingredient.Items
{
    public class ChoppedSpinach : GenericItem<SpinachProvider>
    {
        public override string NameTag => "Chopped Spinach";

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("spinch", "Spinach", "Spinach Stem");
        }
    }
}
