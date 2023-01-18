namespace IngredientLib.Ingredient.Items
{
    public class ChoppedLime : GenericItem<LimeProvider>
    {
        public override string NameTag => "Chopped Lime";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("fruit", "Lime", "Lime Inner", "White Fruit");
        }
    }
}
