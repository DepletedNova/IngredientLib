namespace IngredientLib.Ingredient.Providers
{
    public class MacaroniProvider : GenericProvider
    {
        public override string NameTag => "Macaroni";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Macaroni"))
        };

        public override void OnRegister(Appliance gdo)
        {
            var storage = Prefab.GetChild("MetalStorage");
            storage.ApplyMaterialToChild("Frame", "Metal Dark");
            storage.ApplyMaterialToChild("Platform", "Wood - Default");

            var shelf = Prefab.GetChild("Shelf");
            shelf.ApplyMaterialToChildren("Box", "Plastic - Blue", "Plastic - Orange");
            shelf.ApplyMaterialToChild("Scoop", "Metal");
            shelf.ApplyMaterialToChild("Layer", "Sack");
            shelf.GetChild("Macaroni").ApplyMaterialToChildren("Mac", "Sack");
        }
    }
}
