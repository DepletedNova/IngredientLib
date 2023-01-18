namespace IngredientLib.Ingredient.Items
{
    public class ChoppedPork : GenericItem<PorkProvider>
    {
        public override string NameTag => "Chopped Pork";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("pork", "Pork", "Pork Fat");
        }
    }
}
