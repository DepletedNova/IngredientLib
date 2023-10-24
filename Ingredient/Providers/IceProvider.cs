namespace IngredientLib.Ingredient.Providers
{
    public class IceProvider : GenericProvider
    {
        public override string NameTag => "Ice";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Ice"))
        };

        public override void OnRegister(Appliance gdo)
        {
            SetupLocker(Prefab);
        }
    }
}
