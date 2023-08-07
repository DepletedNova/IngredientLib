namespace IngredientLib.Ingredient.Items
{
    public class SoySauceIngredient : GenericItem
    {
        public override string NameTag => "Soy Sauce";
        public override bool IsSplitItem => true;
        public override GameObject Prefab => GetPrefab("Vinegar");
    }
}
