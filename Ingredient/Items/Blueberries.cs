namespace IngredientLib.Ingredient.Items
{
    public class Blueberries : GenericItem<BlueberryProvider>
    {
        public override string NameTag => "Blueberries";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("scoop", "Metal Black");
            Prefab.GetChild("Berries").ApplyMaterialToChildren("Berry", "Blueberry");
        }
    }
}
