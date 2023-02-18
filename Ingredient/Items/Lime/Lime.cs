namespace IngredientLib.Ingredient.Items
{
    public class Lime : GenericItem<LimeProvider>
    {
        public override string NameTag => "Lime";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override string ColourBlindTag => "Li";

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 1.3f,
                Process = GetGDO<Process>(ProcessReferences.Chop),
                Result = GetCastedGDO<Item, ChoppedLime>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("fruit", "Lime");
        }
    }
}
