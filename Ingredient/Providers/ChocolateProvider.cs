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
            Prefab.ApplyMaterialToChild("Frame", "Metal Dark");
            Prefab.ApplyMaterialToChild("Stand", "Wood - Default");
            var choc = Prefab.GetChild("Chocolate");
            choc.ApplyMaterialToChildren("bar", "Plastic - Red", "Plastic - White");
            choc.ApplyMaterialToChildren("chocolate", "Chocolate");
        }
    }
}
