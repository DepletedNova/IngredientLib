namespace IngredientLib.Ingredient.Items
{
    public class FlourTortilla : GenericItem<FlourTortillaProvider>
    {
        public override string NameTag => "Flour Tortillas";

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Wrap", "Bread - Inside");
            Prefab.ApplyMaterialToChild("Char", "Well-done  Burger");
        }
    }
}
