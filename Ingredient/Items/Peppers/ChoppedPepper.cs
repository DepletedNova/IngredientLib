namespace IngredientLib.Ingredient.Items
{
    public class ChoppedPepper : GenericItem<PepperProvider>
    {
        public override string NameTag => "Chopped Peppers";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override string ColourBlindTag => "Pe";

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("Shaving", "Tomato");
        }
    }
}
