namespace IngredientLib.Ingredient.Providers
{
    public class IceProvider : GenericProvider
    {
        public override string NameTag => "Ice";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Ice"))
        };

        public override void Modify(Appliance gdo)
        {
            SetupLocker(Prefab);

            Prefab.GetChild("Ice").ApplyMaterialToChildren("Ice", "Ice");
        }
    }
}
