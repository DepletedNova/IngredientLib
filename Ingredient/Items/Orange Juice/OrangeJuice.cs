namespace IngredientLib.Ingredient.Items
{
    public class OrangeJuice : GenericItem<OrangeJuiceProvider>
    {
        public override string NameTag => "Orange Juice";
        public override bool IsIndisposable => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;

        public override Item SplitSubItem => GetCastedGDO<Item, OrangeJuiceIngredient>();
        public override int SplitCount => 999;
        public override bool PreventExplicitSplit => true;
        public override bool AllowSplitMerging => true;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Carton", "Plastic - Orange", "Plastic - White", "Hob Black", "Salad Leaf");
        }
    }
}
