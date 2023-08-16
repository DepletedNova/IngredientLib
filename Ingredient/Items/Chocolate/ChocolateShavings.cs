namespace IngredientLib.Ingredient.Items
{
    public class ChocolateShavings : GenericItem<ChocolateProvider>
    {
        public override string NameTag => "Chocolate Shavings";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 0.2f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Result = GetCastedGDO<Item, ChocolateSauce>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("shaving", "Chocolate");
        }
    }
}
