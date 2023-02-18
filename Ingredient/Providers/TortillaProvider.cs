namespace IngredientLib.Ingredient.Providers
{
    public class TortillaProvider : GenericProvider
    {
        public override string NameTag => "Tortillas";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Tortilla"))
        };

        public override void Modify(Appliance gdo)
        {
            var storage = Prefab.GetChild("MetalStorage");
            storage.ApplyMaterialToChild("Frame", "Metal Dark");
            storage.ApplyMaterialToChild("Platform", "Wood - Default");

            var shelf = Prefab.GetChild("Shelf");
            shelf.ApplyMaterialToChild("Sheet", "Paper");
            shelf.GetChild("Tortillas").ApplyMaterialToChildren("Tortilla", "Tortilla", "Tortilla Spots");
            var wrapping = shelf.GetChild("Tortilla_Wrap");
            wrapping.ApplyMaterial("Plastic - Transparent");
            wrapping.ApplyMaterialToChild("Label", "Plastic - Red");
            wrapping.ApplyMaterialToChildren("Tortilla", "Tortilla", "Tortilla Spots");
        }
    }
}
