namespace IngredientLib.Ingredient.Providers
{
    public class AvocadoProvider : GenericProvider
    {
        public override string NameTag => "Avocado";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Avocado"))
        };

        public override void Modify(Appliance gdo)
        {
            SetupGenericCrates(Prefab);
            Prefab.GetChild("Avocados").ApplyMaterialToChildren("avocado", "Avocado");
        }
    }
}
