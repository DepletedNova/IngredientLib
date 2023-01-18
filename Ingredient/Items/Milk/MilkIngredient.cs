namespace IngredientLib.Ingredient.Items
{
    public class MilkIngredient : GenericItem<MilkProvider>
    {
        public override string NameTag => "Milk";
        public override bool IsSplitItem => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
    }
}
