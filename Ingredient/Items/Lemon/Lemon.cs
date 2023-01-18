namespace IngredientLib.Ingredient.Items
{
    public class Lemon : GenericItem<LemonProvider>
    {
        public override string NameTag => "Lemon";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("fruit", "Lemon");
        }
    }
}
