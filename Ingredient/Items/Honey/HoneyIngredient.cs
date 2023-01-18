namespace IngredientLib.Ingredient.Items
{
    public class HoneyIngredient : GenericItem<HoneyProvider>
    {
        public override string NameTag => "Honey";
        public override bool IsSplitItem => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
    }
}
