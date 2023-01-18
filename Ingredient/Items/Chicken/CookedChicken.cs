namespace IngredientLib.Ingredient.Items
{
    public class CookedChicken : GenericItem<ChickenProvider>
    {
        public override string NameTag => "Cooked Chicken";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("chicken", "Cooked Chicken");
        }
    }
}
