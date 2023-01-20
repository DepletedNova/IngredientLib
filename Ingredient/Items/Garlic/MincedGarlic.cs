namespace IngredientLib.Ingredient.Items
{
    public class MincedGarlic : GenericItem<GarlicProvider>
    {
        public override string NameTag => "Minced Garlic";

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("Shaving", "Garlic");
        }
    }
}
