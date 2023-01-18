namespace IngredientLib.Ingredient.Items
{
    public class Lime : GenericItem<LimeProvider>
    {
        public override string NameTag => "Lime";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("fruit", "Lime");
        }
    }
}
