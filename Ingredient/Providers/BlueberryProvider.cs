namespace IngredientLib.Ingredient.Providers
{
    public class BlueberryProvider : GenericProvider
    {
        public override string NameTag => "Blueberries";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Blueberries"))
        };

        public override void Modify(Appliance gdo)
        {
            SetupGenericCrates(Prefab);

            Prefab.GetChild("Berries").ApplyMaterialToChildren("Berry", "Blueberry", "Blueberry 2");
            //Prefab.ApplyMaterialToChild("Pile", "Blueberry Mound");
        }
    }
}
