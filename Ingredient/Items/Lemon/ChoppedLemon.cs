namespace IngredientLib.Ingredient.Items
{
    public class ChoppedLemon : GenericItem<LemonProvider>
    {
        public override string NameTag => "Chopped Lemon";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("fruit", "Lemon", "Lemon Inner", "White Fruit");
        }
    }
}
