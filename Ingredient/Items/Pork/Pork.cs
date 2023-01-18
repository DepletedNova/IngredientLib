namespace IngredientLib.Ingredient.Items
{
    public class Pork : GenericItem<PorkProvider>
    {
        public override string NameTag => "Pork";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("pork", "Pork Fat", "Pork");
        }
    }
}
