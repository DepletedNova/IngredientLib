namespace IngredientLib.Ingredient.Items
{
    public class Bacon : GenericItem<PorkProvider>
    {
        public override string NameTag => "Bacon";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("bacon", "Bacon", "Bacon Fat");
        }
    }
}
