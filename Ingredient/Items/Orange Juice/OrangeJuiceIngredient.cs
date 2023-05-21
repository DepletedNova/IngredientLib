namespace IngredientLib.Ingredient.Items
{
    public class OrangeJuiceIngredient : GenericItem<OrangeJuiceProvider>
    {
        public override string NameTag => "Orange Juice";
        public override bool IsSplitItem => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
    }
}
