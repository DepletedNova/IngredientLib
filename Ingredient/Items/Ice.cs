namespace IngredientLib.Ingredient.Items
{
    public class Ice : GenericItem<IceProvider>
    {
        public override string NameTag => "Ice";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("ice", "Ice");
        }
    }
}
