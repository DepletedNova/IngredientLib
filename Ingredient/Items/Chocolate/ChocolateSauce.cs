namespace IngredientLib.Ingredient.Items
{
    public class ChocolateSauce : GenericItem<ChocolateProvider>
    {
        public override string NameTag => "Chocolate Sauce";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            GameObject bowl = Prefab.GetChild("Small Bowl Soup");
            bowl.ApplyMaterialToChild("Circle", "Plate");
            bowl.ApplyMaterialToChild("Circle.001", "Chocolate Dark");
        }
    }
}
