namespace IngredientLib.Ingredient.Providers
{
    public class LimeProvider : GenericProvider
    {
        public override string NameTag => "Lime";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Lime"))
        };

        public override void Modify(Appliance gdo)
        {
            SetupGenericCrates(Prefab);

            Prefab.GetChild("Limes").ApplyMaterialToChildren("Lime", "Lime");
        }
    }
}
