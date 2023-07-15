namespace IngredientLib.Ingredient.Items
{
    public class Custard : GenericItem
    {
        public override string NameTag => "Custard";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override string ColourBlindTag => "Cu";

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Bowl", "Metal- Shiny");
            Prefab.ApplyMaterialToChild("Filling", "Plastic - Light Yellow", "Plastic - Light Yellow");
        }
    }
}
