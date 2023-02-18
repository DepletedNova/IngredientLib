namespace IngredientLib.Ingredient.Items
{
    public class CookedBonelessDrumstick : GenericItem<DrumstickProvider>
    {
        public override string NameTag => "Cooked Boneless Drumstick";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 6f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                IsBad = true,
                Result = GetGDO<Item>(ItemReferences.BurnedFood)
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("chicken", "Cooked Drumstick");
        }
    }
}
