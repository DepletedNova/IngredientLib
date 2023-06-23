namespace IngredientLib.Ingredient.Items
{
    public class Cinnamon : GenericItem<CinnamonProvider>
    {
        public override string NameTag => "Cinnamon";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("cup", "Metal");
            Prefab.ApplyMaterialToChild("fill", "Cinnamon");
        }
    }
}
