using Kitchen;

namespace IngredientLib.Ingredient.Providers
{
    public class HoneyProvider : GenericProvider
    {
        public override string NameTag => "Honey";
        public override List<Appliance.ApplianceProcesses> Processes => CreateCounterProcesses();
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            new CItemHolder(),
            GetCItemProvider(GetIngredient("Honey"), 1, 1, false, false, true, false, false, true, false)
        };

        public override void OnRegister(Appliance gdo)
        {
            SetupCounter(Prefab);
            SetupCounterLimitedItem(Prefab, GetPrefab("Honey"));

            Prefab.GetChild("Block/HoldPoint/Honey/honey").ApplyMaterial("honey", "Honey", "Cloth - Fancy", "Sack - String");
        }
    }
}
