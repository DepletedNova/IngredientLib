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

        public override List<Item> SplitDepletedItems => new()
        {
            GetCastedGDO<Item, BonelessDrumstick>()
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("meat", "Raw Chicken");
            Prefab.ApplyMaterialToChild("bone", "Raw Drumstick Bone");
        }
    }
}
