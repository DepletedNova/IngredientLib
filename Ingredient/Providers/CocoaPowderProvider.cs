namespace IngredientLib.Ingredient.Providers
{
    public class CocoaPowderProvider : GenericProvider
    {
        public override string NameTag => "Cocoa Powder";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Cocoa Powder"))
        };


        public override void Modify(Appliance gdo)
        {
            Prefab.ApplyMaterialToChild("Sack", "Sack - Brown", "Chocolate");
        }
    }
}
