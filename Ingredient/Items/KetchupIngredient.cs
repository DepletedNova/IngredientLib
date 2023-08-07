namespace IngredientLib.Ingredient.Items
{
    public class KetchupIngredient : GenericItem
    {
        public override string NameTag => "Ketchup";
        public override bool IsSplitItem => true;
        public override GameObject Prefab => GetPrefab("Vinegar");
    }
}
