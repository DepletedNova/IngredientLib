namespace IngredientLib.Ingredient.Items
{
    public class BananaPeel : GenericItem
    {
        public override string NameTag => "Banana Peel";
        public override bool IsSplitItem => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("nanner", "Banana Peel", "Stem", "Banana");
        }
    }
}
