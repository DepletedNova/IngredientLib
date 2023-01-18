namespace IngredientLib.Ingredient.Items
{
    public class Porkchop : GenericItem<PorkProvider>
    {
        public override string NameTag => "Porkchop";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("pork", "Porkchop Fat", "Porkchop");
        }
    }
}
