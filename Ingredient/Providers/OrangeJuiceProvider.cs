namespace IngredientLib.Ingredient.Providers
{
    public class OrangeJuiceProvider : GenericProvider
    {
        public override string NameTag => "Orange Juice";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            new CItemHolder(),
            GetCItemProvider(GetIngredient("Orange Juice"), 1, 1, false, false, true, false, false, true, false),
        };

        public override void OnRegister(Appliance gdo)
        {
            SetupCounter(Prefab);
            SetupCounterLimitedItem(Prefab, GetPrefab("Orange Juice"));

            Prefab.GetChild("Block/HoldPoint/Orange Juice/Carton").ApplyMaterial("Plastic - Orange", "Plastic - White", "Hob Black", "Salad Leaf");
            var crate = Prefab.GetChild("Crate");
            crate.ApplyMaterialToChild("Box", "Wood");
            crate.GetChild("Ice").ApplyMaterialToChildren("Cube", "Ice");
        }
    }
}
