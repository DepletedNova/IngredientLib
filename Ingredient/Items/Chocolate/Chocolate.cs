namespace IngredientLib.Ingredient.Items
{
    public class Chocolate : GenericItem<ChocolateProvider>
    {
        public override string NameTag => "Chocolate";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Bar", "Chocolate", "Chocolate Dark", "Chocolate Light");
        }
    }
}
