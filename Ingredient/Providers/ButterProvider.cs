namespace IngredientLib.Ingredient.Providers
{
    public class ButterProvider : GenericProvider
    {
        public override string NameTag => "Butter";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Butter"))
        };

        public override void Modify(Appliance gdo)
        {
            SetupFridge(Prefab);

            Prefab.GetChildFromPath("Fridge/Fridge2").ApplyMaterialToChildren("Shelf", "Plastic - Grey");
            Prefab.GetChild("Butter").ApplyMaterialToChildren("Butter", "Butter");
        }
    }
}
