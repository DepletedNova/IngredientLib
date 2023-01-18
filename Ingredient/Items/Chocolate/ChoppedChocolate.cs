namespace IngredientLib.Ingredient.Items
{
    public class ChoppedChocolate : GenericItem<ChocolateProvider>
    {
        public override string NameTag => "Chopped Chocolate";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Bar", "Chocolate", "Chocolate Dark", "Chocolate Light");
        }
    }
}
