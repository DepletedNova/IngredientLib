namespace IngredientLib.Ingredient.Items
{
    public class LemonJuice : GenericItem<LemonProvider>
    {
        public override string NameTag => "Lemon Juice";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override string ColourBlindTag => "Le";

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("glass", "Glass");
            Prefab.ApplyMaterialToChild("juice", "Lemon Juice");
        }
    }
}
