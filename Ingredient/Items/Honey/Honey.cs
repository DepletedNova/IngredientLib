namespace IngredientLib.Ingredient.Items
{
    public class Honey : GenericItem<HoneyProvider>
    {
        public override string NameTag => "Honey";
        public override bool IsIndisposable => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;

        public override Item SplitSubItem => GetCastedGDO<Item, HoneyIngredient>();
        public override bool PreventExplicitSplit => true;
        public override bool AllowSplitMerging => true;
        public override int SplitCount => 999;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("honey", "Honey", "Cloth - Fancy", "Sack - String");
        }
    }
}
