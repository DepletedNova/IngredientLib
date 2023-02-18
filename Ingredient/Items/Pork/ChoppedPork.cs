namespace IngredientLib.Ingredient.Items
{
    public class ChoppedPork : GenericItem<PorkProvider>
    {
        public override string NameTag => "Chopped Pork";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 4f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Result = GetCastedGDO<Item, Bacon>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("pork", "Pork", "Pork Fat");
        }
    }
}
