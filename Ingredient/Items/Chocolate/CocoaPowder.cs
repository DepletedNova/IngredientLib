namespace IngredientLib.Ingredient.Items
{
    public class CocoaPowder : GenericItem<CocoaPowderProvider>
    {
        public override string NameTag => "Cocoa Powder";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Cup", "Metal");
            Prefab.ApplyMaterialToChild("Fill", "Chocolate");
        }
    }
}
