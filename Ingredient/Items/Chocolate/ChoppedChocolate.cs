namespace IngredientLib.Ingredient.Items
{
    public class ChoppedChocolate : GenericItem<ChocolateProvider>
    {
        public override string NameTag => "Chopped Chocolate";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 0.5f,
                Process = GetGDO<Process>(ProcessReferences.Chop),
                Result = GetCastedGDO<Item, ChocolateShavings>()
            },
            new()
            {
                Duration = 0.6f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Result = GetCastedGDO<Item, ChocolateSauce>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Bar", "Chocolate", "Chocolate Dark", "Chocolate Light");
        }
    }
}
