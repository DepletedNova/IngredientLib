namespace IngredientLib.Ingredient.Items
{
    public class MashedAvocado : GenericItem<AvocadoProvider>
    {
        public override string NameTag => "Mashed Avocado";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("avocado", "Avocado Inside");
            Prefab.ApplyMaterialToChild("bowl", "Wood 1 - Dim");
        }
    }
}
