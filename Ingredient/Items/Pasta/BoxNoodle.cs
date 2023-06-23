namespace IngredientLib.Ingredient.Items
{
    public class BoxNoodle : GenericItem<BoxPastaProvider>
    {
        public override string NameTag => "Box Pasta";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("pasta", "Sack");
        }
    }
}
