﻿namespace IngredientLib.Ingredient.Providers
{
    public class PorkProvider : GenericProvider
    {
        public override string NameTag => "Pork";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Pork"))
        };

        public override void OnRegister(Appliance gdo)
        {
            SetupFridge(Prefab);

            Prefab.GetChild("Fridge/Fridge2/Shelf").ApplyMaterial("Plastic - Grey");

            Prefab.GetChild("Porks").ApplyMaterialToChildren("Pork", "Pork", "Pork Fat");
        }
    }
}
