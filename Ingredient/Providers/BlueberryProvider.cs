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
            Prefab.ApplyMaterialToChild("Frame", "Metal Dark");
            Prefab.ApplyMaterialToChild("Stand", "Wood - Default");
            Prefab.ApplyMaterialToChild("Baskets", "Plastic - Blue");
            Prefab.GetChild("Blueberries").ApplyMaterialToChildren("Blueberry", "Blueberry");
        }
    }
}
