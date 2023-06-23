namespace IngredientLib.Ingredient.Items
{
    public class CookedMacaroniPot : GenericItem
    {
        public override string NameTag => "Cooked Potted Macaroni";
        public override Item DisposesTo => GetGDO<Item>(ItemReferences.Pot);
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Handles", "Metal Dark");
            Prefab.ApplyMaterialToChild("Pot", "Metal");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
            Prefab.GetChild("Macaroni").ApplyMaterialToChildren("Mac", "Raw Pastry");
        }
    }
}
