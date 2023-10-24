namespace IngredientLib.Ingredient.Providers
{
    public class DrumstickProvider : GenericProvider
    {
        public override string NameTag => "Drumstick";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Drumstick"))
        };

        public override void OnRegister(Appliance gdo)
        {
            SetupLocker(Prefab);

            Prefab.GetChild("Chicken").ApplyMaterialToChildren("chicken", "Raw Chicken");
            Prefab.GetChild("Chicken").ApplyMaterialToChildren("bone", "Raw Drumstick Bone");
        }
    }
}
