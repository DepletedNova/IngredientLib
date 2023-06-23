namespace IngredientLib.Ingredient.Providers
{
    public class BoxPastaProvider : GenericProvider
    {
        public override string NameTag => "Box Pasta";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Box Pasta"))
        };

        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Noodles", "Provides noodles", new(), new()))
        };

        public override void Modify(Appliance gdo)
        {
            Prefab.ApplyMaterialToChild("Sack", "Sack - Brown");
            Prefab.GetChild("Noodles").ApplyMaterialToChildren("Noodle", "Sack");
        }
    }
}
