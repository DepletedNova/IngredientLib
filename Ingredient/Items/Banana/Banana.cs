namespace IngredientLib.Ingredient.Items
{
    public class Banana : GenericItem<BananaProvider>
    {
        public override string NameTag => "Banana";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override Item SplitSubItem => GetCastedGDO<Item, BananaPeel>();
        public override int SplitCount => 1;
        public override float SplitSpeed => 1f;

        public override void Modify(Item gdo)
        {
            gdo.SplitDepletedItems.Add(GetCastedGDO<Item, PeeledBanana>());

            Prefab.ApplyMaterialToChild("nanner", "Banana Peel", "Stem");
        }
    }
}
