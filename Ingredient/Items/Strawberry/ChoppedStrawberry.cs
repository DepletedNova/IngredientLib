namespace IngredientLib.Ingredient.Items
{
    public class ChoppedStrawberry : GenericItem
    {
        public override string NameTag => "Chopped Strawberry";

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("strawberry", "Strawberry", "Strawberry Inside");
        }
    }
}
