namespace IngredientLib.Ingredient.Providers
{
    public class FlourTortillaProvider : GenericProvider
    {
        public override string NameTag => "Flour Tortillas";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Flour Tortillas"))
        };

        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Tortillas", "Provides tortillas", new(), new()))
        };

        public override void OnRegister(Appliance gdo)
        {
            SetupCounter(Prefab);

            var box = Prefab.GetChild("Breadbox");
            box.ApplyMaterialToChild("Box", "Wood");
            box.ApplyMaterialToChild("Door", "Wood", "Metal");

            Prefab.GetChild("Tortillas").ApplyMaterialToChildren("Tortilla", "Bread - Inside", "Well-done  Burger");

            ReflectionUtils.GetField<ItemSourceView>("Animator").SetValue(Prefab.TryAddComponent<ItemSourceView>(), box.GetComponent<Animator>());
        }
    }
}
