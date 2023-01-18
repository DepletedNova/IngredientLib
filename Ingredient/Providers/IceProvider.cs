namespace IngredientLib.Ingredient.Providers
{
    public class IceProvider : GenericProvider
    {
        public override string NameTag => "Ice";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            new CItemHolder(),
            GetCItemProvider(GetIngredient("Ice"), 1, 1, false, false, true, false, false, true, false)
        };

        public override void Modify(Appliance gdo)
        {
            SetupLocker(Prefab);

            Prefab.GetChild("Ice").ApplyMaterialToChildren("Ice", "Ice");
        }
    }
}
