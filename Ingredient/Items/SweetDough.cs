namespace IngredientLib.Ingredient.Items
{
    public class SweetDough : GenericItem
    {
        public override string NameTag => "Sweet Dough";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override string ColourBlindTag => "SD";

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Bowl", "Metal");
            Prefab.ApplyMaterialToChild("Dough", "Raw Pastry");
        }
    }
}
