namespace IngredientLib.Ingredient.Providers
{
    public class ChickenProvider : GenericProvider
    {
        public override string NameTag => "Chicken";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Chicken"))
        };

        public override void OnRegister(Appliance gdo)
        {
            SetupLocker(Prefab);

            Prefab.GetChild("Chicken").ApplyMaterialToChildren("chicken", "Raw Chicken");
        }
    }
}
