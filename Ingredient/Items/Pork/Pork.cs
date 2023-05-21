namespace IngredientLib.Ingredient.Items
{
    public class Pork : GenericItem<PorkProvider>
    {
        public override string NameTag => "Pork";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 6.5f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Result = GetCastedGDO<Item, Porkchop>()
            },
            new()
            {
                Duration = 1.7f,
                Process = GetGDO<Process>(ProcessReferences.Chop),
                Result = GetCastedGDO<Item, ChoppedPork>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("pork", "Pork Fat", "Pork");
        }
    }
}
