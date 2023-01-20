namespace IngredientLib.Ingredient.Providers
{
    public class BoxPastaProvider : GenericProvider
    {
        public override string NameTag => "Box Pasta";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Box Pasta"))
        };

        public override void Modify(Appliance gdo)
        {
            var storage = Prefab.GetChild("MetalStorage");
            storage.ApplyMaterialToChild("Frame", "Metal Dark");
            storage.ApplyMaterialToChild("Platform", "Wood - Default");

            var shelf = Prefab.GetChild("Shelf");
            shelf.ApplyMaterialToChild("Box", "Plastic - Blue", "Plastic - Orange", "Emoji Yellow", "Emoji Orange");
            shelf.ApplyMaterialToChildren("noodle", "Sack");
            shelf.ApplyMaterialToChild("Sheet", "Paper - White");
        }
    }
}
