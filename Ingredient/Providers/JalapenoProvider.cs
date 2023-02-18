namespace IngredientLib.Ingredient.Providers
{
    public class JalapenoProvider : GenericProvider
    {
        public override string NameTag => "Jalapeños";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Jalapeños"))
        };

        public override void Modify(Appliance gdo)
        {
            SetupGenericCrates(Prefab);

            Prefab.GetChild("Peppers").ApplyMaterialToChildren("Pepper", "Jalapeno", "Salad Leaf", "Stem");
        }
    }
}
