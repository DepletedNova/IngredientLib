namespace IngredientLib.Ingredient.Providers
{
    public class PepperProvider : GenericProvider
    {
        public override string NameTag => "Peppers";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Peppers"))
        };

        public override void Modify(Appliance gdo)
        {
            SetupGenericCrates(Prefab);

            Prefab.GetChild("Peppers").ApplyMaterialToChildren("Pepper", "Tomato", "Salad Leaf");
        }
    }
}
