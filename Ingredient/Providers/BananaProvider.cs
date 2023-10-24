namespace IngredientLib.Ingredient.Providers
{
    public class BananaProvider : GenericProvider
    {
        public override string NameTag => "Banana";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Banana"))
        };


        public override void OnRegister(Appliance gdo)
        {
            SetupGenericCrates(Prefab);

            Prefab.GetChild("Bananas").ApplyMaterialToChildren("Banana", "Banana Peel", "Stem");
        }
    }
}
