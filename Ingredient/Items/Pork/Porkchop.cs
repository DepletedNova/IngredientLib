namespace IngredientLib.Ingredient.Items
{
    public class Porkchop : GenericItem<PorkProvider>
    {
        public override string NameTag => "Porkchop";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 10f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                IsBad = true,
                Result = GetCastedGDO<Item, BurnedPorkchop>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("pork", "Porkchop Fat", "Porkchop");
        }
    }
}
