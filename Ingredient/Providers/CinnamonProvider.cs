namespace IngredientLib.Ingredient.Providers
{
    public class CinnamonProvider : GenericProvider
    {
        public override string NameTag => "Cinnamon";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Cinnamon"))
        };

        public override void OnRegister(Appliance gdo)
        {
            Prefab.ApplyMaterialToChild("Sack", "Cloth - Fancy", "Cinnamon");
        }
    }
}
