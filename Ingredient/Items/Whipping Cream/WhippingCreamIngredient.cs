namespace IngredientLib.Ingredient.Items
{
    public class WhippingCreamIngredient : GenericItem<WhippingCreamProvider>
    {
        public override string NameTag => "Whipping Cream";
        public override bool IsSplitItem => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
    }
}
