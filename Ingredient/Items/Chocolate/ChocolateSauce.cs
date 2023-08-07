namespace IngredientLib.Ingredient.Items
{
    public class ChocolateSauce : GenericItem<ChocolateProvider>
    {
        public override string NameTag => "Chocolate Sauce";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override string ColourBlindTag => "Sa";

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 15f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                IsBad = true,
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Bowl", "Metal");
            Prefab.ApplyMaterialToChild("Chocolate", "Chocolate");

            this.Redirect(-1004033684); // Melted Chocolate
        }
    }
}
