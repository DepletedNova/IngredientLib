namespace IngredientLib.Util.Custom
{
    public abstract class GenericProvider : CustomAppliance
    {
        public abstract string NameTag { get; }

        public override string UniqueNameID => $"{NameTag.ToLower()} source";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo(NameTag, $"Provides {NameTag}", new List<Appliance.Section>(), new()))
        };
        public override bool SellOnlyAsDuplicate => true;
        public override bool IsPurchasable => true;
        public override PriceTier PriceTier => PriceTier.Medium;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking | ShoppingTags.Misc;
        public override GameObject Prefab => GetPrefab($"{NameTag} Source");

        public override void Convert(GameData gameData, out GameDataObject gdo)
        {
            base.Convert(gameData, out gdo);

            AddProvider(NameTag, gdo);
        }

        public override void AttachDependentProperties(GameData gameData, GameDataObject gdo)
        {
            base.AttachDependentProperties(gameData, gdo);

            AddCustomProperties();
        }

        public override void OnRegister(Appliance gdo) => Modify(gdo);

        protected virtual void AddCustomProperties() { }
        public abstract void Modify(Appliance gdo);
    }
}
