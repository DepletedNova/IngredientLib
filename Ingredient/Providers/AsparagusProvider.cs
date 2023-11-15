namespace IngredientLib.Ingredient.Providers
{
    public class AsparagusProvider : GenericProvider
    {
        public override string NameTag => "Asparagus";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Asparagus"))
        };

        public override void OnRegister(Appliance gdo)
        {
            Prefab.ApplyMaterialToChild("Vase", "Vase - Dark", "Vase", "Soil");
            Prefab.GetChild("Asparagus").ApplyMaterialToChildren("A", "Plant", "Bamboo Leaf");
        }
    }
}
