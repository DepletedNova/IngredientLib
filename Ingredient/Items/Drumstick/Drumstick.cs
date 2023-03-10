namespace IngredientLib.Ingredient.Items
{
    public class Drumstick : GenericItem<DrumstickProvider>
    {
        public override string NameTag => "Drumstick";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override Item SplitSubItem => GetCastedGDO<Item, DrumstickBone>();
        public override int SplitCount => 1;
        public override float SplitSpeed => 1f;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 6f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Result = GetCastedGDO<Item, CookedDrumstick>()
            }
        };

        public override void Modify(Item gdo)
        {
            gdo.SplitDepletedItems.Add(GetCastedGDO<Item, BonelessDrumstick>());

            Prefab.ApplyMaterialToChild("chicken", "Raw Chicken", "Raw Drumstick Bone");
        }
    }
}
