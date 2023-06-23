namespace IngredientLib.Ingredient.Items
{
    public class WhippedCream : GenericItem<WhippingCreamProvider>
    {
        public override string NameTag => "Whipped Cream";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Bowl", "Metal - Brass");
            Prefab.ApplyMaterialToChild("Pile", "Plastic - White", "Plastic - White");
        }
    }
}
