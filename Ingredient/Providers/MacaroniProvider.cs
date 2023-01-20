namespace IngredientLib.Ingredient.Providers
{
    public class MacaroniProvider : GenericProvider
    {
        public override string NameTag => "Macaroni";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Macaroni"))
        };

        public override void Modify(Appliance gdo)
        {
            var storage = Prefab.GetChild("MetalStorage");
            storage.ApplyMaterialToChild("Frame", "Metal Dark");
            storage.ApplyMaterialToChild("Platform", "Wood - Default");

            var shelf = Prefab.GetChild("Shelf");
            shelf.ApplyMaterialToChildren("Box", "Plastic - Blue", "Emoji Yellow", "Plastic - Orange", "Plastic - Black Dark");
            shelf.ApplyMaterialToChild("scoop", "Metal");
            shelf.ApplyMaterialToChild("mac", "Sack");
        }
    }
}
