namespace IngredientLib.Ingredient.Providers
{
    public class SpinachProvider : GenericProvider
    {
        public override string NameTag => "Spinach";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Spinach"))
        };

        public override void Modify(Appliance gdo)
        {
            SetupGenericCrates(Prefab);

            GameObject parent = Prefab.GetChild("Spinch");
            parent.ApplyMaterialToChildren("spinch", "Spinach");
            parent.ApplyMaterialToChildren("band", "Plastic - Red");
        }
    }
}
