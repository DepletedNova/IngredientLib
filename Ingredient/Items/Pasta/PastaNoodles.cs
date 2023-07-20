namespace IngredientLib.Ingredient.Items
{
    public class PastaNoodles : GenericItem
    {
        public override string NameTag => "Pasta Noodles";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("pasta", "Raw Pastry", "Raw Pastry");
        }
    }
}
