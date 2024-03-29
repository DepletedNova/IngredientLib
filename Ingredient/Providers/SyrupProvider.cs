﻿namespace IngredientLib.Ingredient.Providers
{
    public class SyrupProvider : GenericProvider
    {
        public override string NameTag => "Syrup";
        public override List<Appliance.ApplianceProcesses> Processes => CreateCounterProcesses();
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            new CItemHolder(),
            GetCItemProvider(GetIngredient("Syrup"), 1, 1, false, false, true, false, false, true, false)
        };

        public override void OnRegister(Appliance gdo)
        {
            SetupCounter(Prefab);
            SetupCounterLimitedItem(Prefab, GetPrefab("Syrup"));

            Prefab.GetChild("Block/HoldPoint/Syrup/bottle").ApplyMaterial("Metal- Shiny Blue", "Cooked Batter", "Paper", "Plastic - Red");
        }
    }
}
