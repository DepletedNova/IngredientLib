namespace IngredientLib.Ingredient.Providers
{
    public class VinegarProvider : GenericProvider
    {
        public override string NameTag => "Vinegar";
        public override List<Appliance.ApplianceProcesses> Processes => CreateCounterProcesses();
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            new CItemHolder(),
            GetCItemProvider(GetIngredient("Vinegar"), 1, 1, false, false, true, false, false, true, false)
        };

        public override void OnRegister(Appliance gdo)
        {
            SetupCounter(Prefab);
            SetupCounterLimitedItem(Prefab, GetPrefab("Vinegar"));

            Prefab.GetChild("Block/HoldPoint/Vinegar/Jar").ApplyMaterial("Vinegar", "Plastic - White", "Metal");
        }
    }
}
