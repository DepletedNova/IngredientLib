namespace IngredientLib.Ingredient.Items
{
    public class CookedBonelessDrumstick : GenericItem<DrumstickProvider>
    {
        public override string NameTag => "Cooked Boneless Drumstick";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("chicken", "Cooked Drumstick");
        }
    }
}
