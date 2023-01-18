namespace IngredientLib.Ingredient.Items
{
    public class CookedDrumstick : GenericItem<DrumstickProvider>
    {
        public override string NameTag => "Cooked Drumstick";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("chicken", "Cooked Drumstick", "Cooked Drumstick Bone");
        }
    }
}
