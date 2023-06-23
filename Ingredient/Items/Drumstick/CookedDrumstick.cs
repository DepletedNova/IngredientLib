namespace IngredientLib.Ingredient.Items
{
    public class CookedDrumstick : GenericItem<DrumstickProvider>
    {
        public override string NameTag => "Cooked Drumstick";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 10f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                IsBad = true,
                Result = GetGDO<Item>(ItemReferences.BurnedFood)
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("meat", "Cooked Drumstick");
            Prefab.ApplyMaterialToChild("bone", "Cooked Drumstick Bone");
        }
    }
}
