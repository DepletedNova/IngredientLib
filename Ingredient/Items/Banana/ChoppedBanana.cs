namespace IngredientLib.Ingredient.Items
{
    public class ChoppedBanana : GenericItem<BananaProvider>
    {
        public override string NameTag => "Chopped Banana";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("banana", "Banana", "Banana Inner");
        }
    }
}
