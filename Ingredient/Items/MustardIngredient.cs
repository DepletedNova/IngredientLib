namespace IngredientLib.Ingredient.Items
{
    public class MustardIngredient : GenericItem
    {
        public override string NameTag => "Mustard";
        public override bool IsSplitItem => true;
        public override GameObject Prefab => GetPrefab("Vinegar");
    }
}
