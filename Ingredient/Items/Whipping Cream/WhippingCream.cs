namespace IngredientLib.Ingredient.Items
{
    public class WhippingCream : GenericItem<WhippingCreamProvider>
    {
        public override string NameTag => "Whipping Cream";
        public override bool IsIndisposable => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;

        public override Item SplitSubItem => GetCastedGDO<Item, WhippingCreamIngredient>();
        public override int SplitCount => 999;
        public override float SplitSpeed => 2.0f;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Jar", "Plastic - Red", "Plastic - White");
        }
    }
}
