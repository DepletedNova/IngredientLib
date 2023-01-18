namespace IngredientLib.Ingredient.Items
{
    public class BurnedPorkchop : GenericItem
    {
        public override string NameTag => "Burned Porkchop";
        public override ItemStorage ItemStorageFlags => ItemStorage.None;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("pork", "Burned", "Burned - Light");
        }
    }
}
