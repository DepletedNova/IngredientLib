namespace IngredientLib.Ingredient.Items
{
    public class PeeledBanana : GenericItem<BananaProvider>
    {
        public override string NameTag => "Peeled Banana";

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 1.3f,
                Process = GetGDO<Process>(ProcessReferences.Chop),
                Result = GetCastedGDO<Item, ChoppedBanana>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("nanner", "Banana");
        }
    }
}
