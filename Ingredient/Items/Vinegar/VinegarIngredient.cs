namespace IngredientLib.Ingredient.Items
{
    public class VinegarIngredient : GenericItem<VinegarProvider>
    {
        public override string NameTag => "Vinegar";
        public override bool IsSplitItem => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
    }
}
