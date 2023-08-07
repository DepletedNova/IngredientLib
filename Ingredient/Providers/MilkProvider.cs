namespace IngredientLib.Ingredient.Providers
{
    public class MilkProvider : GenericProvider
    {
        public override string NameTag => "Milk";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            new CItemHolder(),
            GetCItemProvider(GetIngredient("Milk"), 1, 1, false, false, true, false, false, true, false)
        };

        public override void Modify(Appliance gdo)
        {
            SetupCounter(Prefab);
            SetupCounterLimitedItem(Prefab, GetPrefab("Milk"));

            Prefab.GetChild("Block/HoldPoint/Milk/carton").ApplyMaterial("Plastic - Blue", "Plastic - White");
            var crate = Prefab.GetChild("Crate");
            crate.ApplyMaterialToChild("Box", "Wood");
            crate.GetChild("Ice").ApplyMaterialToChildren("Cube", "Ice");

            this.Redirect(ApplianceReferences.SourceMilk);
        }
    }
}
