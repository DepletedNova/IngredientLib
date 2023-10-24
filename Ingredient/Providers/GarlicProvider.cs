namespace IngredientLib.Ingredient.Providers
{
    public class GarlicProvider : GenericProvider
    {
        public override string NameTag => "Garlic";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Garlic"))
        };

        public override void OnRegister(Appliance gdo)
        {
            SetupGenericCrates(Prefab);

            Prefab.GetChild("Garlic").ApplyMaterialToChildren("Garlic", "Garlic");
        }
    }
}
