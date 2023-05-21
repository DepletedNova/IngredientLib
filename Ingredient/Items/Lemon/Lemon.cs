namespace IngredientLib.Ingredient.Items
{
    public class Lemon : GenericItem<LemonProvider>
    {
        public override string NameTag => "Lemon";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override string ColourBlindTag => "Le";

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 0.6f,
                Process = GetGDO<Process>(ProcessReferences.Chop),
                Result = GetCastedGDO<Item, ChoppedLemon>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("fruit", "Lemon");
        }
    }
}
