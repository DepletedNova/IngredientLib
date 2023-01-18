namespace IngredientLib.Ingredient.Items
{
    public class BonelessDrumstick : GenericItem<DrumstickProvider>
    {
        public override string NameTag => "Boneless Drumstick";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("chicken", "Raw Chicken");
        }
    }
}
