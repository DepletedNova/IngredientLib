namespace IngredientLib.Ingredient.Providers
{
    public class ChocolateProvider : GenericProvider
    {
        public override string NameTag => "Chocolate";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Chocolate"))
        };

        public override void Modify(Appliance gdo)
        {
            SetupGenericCrates(Prefab);

            GameObject parent = Prefab.GetChild("Bars");
            parent.ApplyMaterialToChildren("WrappedFull", "Plastic - Red", "Plastic - White");
            parent.ApplyMaterialToChildren("WrappedHalf", "Chocolate", "Chocolate Dark", "Chocolate Light", "Plastic - Red", "Plastic - White", "Plastic");
            parent.ApplyMaterialToChild("Chocolate", "Chocolate", "Chocolate Dark", "Chocolate Light");
        }
    }
}
