namespace IngredientLib.Ingredient.Items
{
    public class WhippedCream : GenericItem<WhippingCreamProvider>
    {
        public override string NameTag => "Whipped Cream";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Bowl", "Door Glass");
            Prefab.ApplyMaterialToChild("Pile", "Coffee Cup");
            Prefab.ApplyMaterialToChild("Bubbling", "Coffee Cup");
        }
    }
}
