namespace IngredientLib.Ingredient.Providers
{
    public class WhippingCreamProvider : GenericProvider
    {
        public override string NameTag => "Whipping Cream";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            new CItemHolder(),
            GetCItemProvider(GetIngredient("Whipping Cream"), 1, 1, false, false, true, false, false, true, false)
        };

        public override void Modify(Appliance gdo)
        {
            SetupCounter(Prefab, "Whipping Cream - Item");

            Prefab.GetChildFromPath("Block/HoldPoint/Whipping Cream - Item/Jar").ApplyMaterial("Plastic - White", "Plastic - Red");
            var crate = Prefab.GetChild("Crate");
            crate.ApplyMaterialToChild("Box", "Wood");
            crate.GetChild("Ice").ApplyMaterialToChildren("Cube", "Ice");
        }
    }
}
