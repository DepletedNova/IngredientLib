namespace IngredientLib.Ingredient.Providers
{
    public class WhippingCreamProvider : GenericProvider
    {
        public override string NameTag => "Whipping Cream";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            new CItemHolder(),
            GetCItemProvider(GetIngredient("Whipping Cream"), 1, 1, false, false, true, false, false, true, false)
        };

        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Heavy Cream", "Provides heavy cream", new(), new()))
        };

        public override void OnRegister(Appliance gdo)
        {
            SetupCounter(Prefab);
            SetupCounterLimitedItem(Prefab, GetPrefab("Whipping Cream"));

            Prefab.GetChild("Block/HoldPoint/Whipping Cream/Jar").ApplyMaterial("Plastic - Red", "Plastic - White");
            var crate = Prefab.GetChild("Crate");
            crate.ApplyMaterialToChild("Box", "Wood");
            crate.GetChild("Ice").ApplyMaterialToChildren("Cube", "Ice");
        }
    }
}
