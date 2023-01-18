namespace IngredientLib.Ingredient.Items
{
    public class Pepper : GenericItem<PepperProvider>
    {
        public override string NameTag => "Peppers";
        public override ItemStorage ItemStorageFlags => ItemStorage.None;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("jalapeno", "Tomato", "Salad Leaf", "Stem");
        }
    }
}
