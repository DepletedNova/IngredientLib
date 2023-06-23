namespace IngredientLib.Ingredient.Items
{
    public class FlourTortilla : GenericItem<FlourTortillaProvider>
    {
        public override string NameTag => "Flour Tortillas";

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("tortilla", "Bread - Inside", "Well-done  Burger");
        }
    }
}
