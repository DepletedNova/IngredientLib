namespace IngredientLib.Ingredient.Items
{
    public class ChocolateShavings : GenericItem<ChocolateProvider>
    {
        public override string NameTag => "Chocolate Shavings";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("shaving", "Chocolate");
        }
    }
}
