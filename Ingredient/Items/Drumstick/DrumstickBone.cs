namespace IngredientLib.Ingredient.Items
{
    public class DrumstickBone : GenericItem<DrumstickProvider>
    {
        public override string NameTag => "Drumstick Bone";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("chicken", "Raw Drumstick Bone");
        }
    }
}
