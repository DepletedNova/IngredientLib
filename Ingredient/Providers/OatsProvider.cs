namespace IngredientLib.Ingredient.Providers
{
    public class OatsProvider : GenericProvider
    {
        public override string NameTag => "Oats";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Oats"))
        };

        public override void Modify(Appliance gdo)
        {
            var sack = Prefab.GetChild("Sack");
            sack.ApplyMaterial("Sack");
            sack.ApplyMaterialToChildren("Grain", "Oat Grain");
            sack.ApplyMaterialToChildren("Mound", "Oat Mound");
        }
    }
}
