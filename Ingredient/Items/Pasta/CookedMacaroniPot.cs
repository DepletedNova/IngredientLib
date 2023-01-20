namespace IngredientLib.Ingredient.Items
{
    public class CookedMacaroniPot : GenericItem
    {
        public override string NameTag => "Cooked Potted Macaroni";
        public override GameObject Prefab => GetPrefab("Potted Pasta");
        public override Item DisposesTo => GetGDO<Item>(ItemReferences.Pot);
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
    }
}
