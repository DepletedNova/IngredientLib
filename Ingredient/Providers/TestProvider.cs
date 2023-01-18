namespace IngredientLib.Ingredient.Providers
{
    public class TestProvider : GenericProvider
    {
        public override string NameTag => "Test Provider";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Banana"))
        };

        public override void Modify(Appliance gdo)
        {

        }

        protected override void AddCustomProperties()
        {
            /*DualProviderRegistry.AddProvider(this, new CDualProvider()
            {
                Current = 0,
                Provide1 = GetIngredient("Banana"),
                Provide2 = GetIngredient("Peppers")
            });*/
        }
    }
}
