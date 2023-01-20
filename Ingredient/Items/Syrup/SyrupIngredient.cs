namespace IngredientLib.Ingredient.Items
{
    public class SyrupIngredient : GenericItem<SyrupProvider>
    {
        public override string NameTag => "Syrup";
        public override bool IsSplitItem => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
    }
}
