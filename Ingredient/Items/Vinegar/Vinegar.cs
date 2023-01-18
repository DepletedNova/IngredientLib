namespace IngredientLib.Ingredient.Items
{
    public class Vinegar : GenericItem<VinegarProvider>
    {
        public override string NameTag => "Vinegar";
        public override bool IsIndisposable => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;

        public override Item SplitSubItem => GetCastedGDO<Item, VinegarIngredient>();
        public override bool PreventExplicitSplit => true;
        public override bool AllowSplitMerging => true;
        public override int SplitCount => 999;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Jar", "Cork", "Vinegar", "Cardboard");
        }
    }
}
