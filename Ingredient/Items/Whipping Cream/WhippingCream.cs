namespace IngredientLib.Ingredient.Items
{
    public class WhippingCream : GenericItem<WhippingCreamProvider>
    {
        public override string NameTag => "Whipping Cream";
        public override bool IsIndisposable => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override Item SplitSubItem => GetCastedGDO<Item, WhippingCreamIngredient>();
        public override bool PreventExplicitSplit => true;
        public override bool AllowSplitMerging => true;
        public override int SplitCount => 999;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Jar", "Plastic - White", "Plastic - Red");
        }
    }
}
