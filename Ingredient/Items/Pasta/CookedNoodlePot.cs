namespace IngredientLib.Ingredient.Items
{
    public class CookedNoodlePot : GenericItem
    {
        public override string NameTag => "Cooked Potted Pasta";
        public override Item DisposesTo => GetGDO<Item>(ItemReferences.Pot);
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;

        public override int SplitCount => 3;
        public override Item SplitSubItem => GetCastedGDO<Item, PastaNoodles>();
        public override List<Item> SplitDepletedItems => new() { GetGDO<Item>(ItemReferences.Pot) };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Handles", "Metal Dark");
            Prefab.ApplyMaterialToChild("Pot", "Metal");
            Prefab.ApplyMaterialToChild("Pasta", "Flour Bag", "Raw Pastry");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
        }
    }
}
