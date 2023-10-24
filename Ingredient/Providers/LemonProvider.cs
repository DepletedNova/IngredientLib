namespace IngredientLib.Ingredient.Providers
{
    public class LemonProvider : GenericProvider
    {
        public override string NameTag => "Lemon";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Lemon"))
        };

        public override void OnRegister(Appliance gdo)
        {
            SetupGenericCrates(Prefab);

            Prefab.GetChild("Lemons").ApplyMaterialToChildren("Lemon", "Lemon");

            this.Redirect(1470180731);
        }
    }
}
