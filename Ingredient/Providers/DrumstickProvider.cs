namespace IngredientLib.Ingredient.Providers
{
    public class DrumstickProvider : GenericProvider
    {
        public override string NameTag => "Drumstick";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Drumstick"))
        };

        public override void Modify(Appliance gdo)
        {
            SetupLocker(Prefab);

            Prefab.GetChild("Ice").ApplyMaterialToChildren("Ice", "Ice");
            Prefab.GetChild("Chicken").ApplyMaterialToChildren("chicken", "Raw Chicken", "Raw Drumstick Bone");
        }
    }
}
