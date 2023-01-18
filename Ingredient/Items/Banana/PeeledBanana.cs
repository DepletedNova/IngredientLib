namespace IngredientLib.Ingredient.Items
{
    public class PeeledBanana : GenericItem<BananaProvider>
    {
        public override string NameTag => "Peeled Banana";
        public override ItemStorage ItemStorageFlags => ItemStorage.None;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("nanner", "Banana");
        }
    }
}
