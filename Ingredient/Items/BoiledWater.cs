namespace IngredientLib.Ingredient.Items
{
    public class BoiledWater : GenericItem
    {
        public override string NameTag => "Boiled Water";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override GameObject Prefab => GetPrefab("Ice");
    }
}
