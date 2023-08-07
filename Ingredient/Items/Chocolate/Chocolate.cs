namespace IngredientLib.Ingredient.Items
{
    public class Chocolate : GenericItem<ChocolateProvider>
    {
        public override string NameTag => "Chocolate";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 1f,
                Process = GetGDO<Process>(ProcessReferences.Chop),
                Result = GetCastedGDO<Item, ChoppedChocolate>()
            },
            new()
            {
                Duration = 2.4f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Result = GetCastedGDO<Item, ChocolateSauce>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Bar", "Chocolate");

            this.Redirect(106900119); // Chocolate
        }
    }
}
