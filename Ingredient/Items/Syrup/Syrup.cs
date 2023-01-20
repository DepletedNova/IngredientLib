namespace IngredientLib.Ingredient.Items
{
    public class Syrup : GenericItem<SyrupProvider>
    {
        public override string NameTag => "Syrup";
        public override bool IsIndisposable => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;

        public override Item SplitSubItem => GetCastedGDO<Item, SyrupIngredient>();
        public override bool PreventExplicitSplit => true;
        public override bool AllowSplitMerging => true;
        public override int SplitCount => 999;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("bottle", "Cooked Batter", "Metal- Shiny Blue", "Plastic - Red", "Paper");
        }
    }
}
