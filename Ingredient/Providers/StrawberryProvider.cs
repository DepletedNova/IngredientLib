namespace IngredientLib.Ingredient.Providers
{
    public class StrawberryProvider : GenericProvider
    {
        public override string NameTag => "Strawberry";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Strawberry"))
        };

        public override void Modify(Appliance gdo)
        {
            Prefab.ApplyMaterialToChild("Frame", "Metal Dark");
            Prefab.ApplyMaterialToChild("Stand", "Wood - Default");
            Prefab.ApplyMaterialToChild("Baskets", "Plastic - Blue");
            Prefab.GetChild("Strawberries").ApplyMaterialToChildren("Strawberry", "Strawberry");
        }
    }
}
