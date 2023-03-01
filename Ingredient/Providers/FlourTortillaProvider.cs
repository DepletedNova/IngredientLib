namespace IngredientLib.Ingredient.Providers
{
    public class FlourTortillaProvider : GenericProvider
    {
        public override string NameTag => "Flour Tortillas";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Flour Tortillas"))
        };

        public override void Modify(Appliance gdo)
        {
            SetupCounter(Prefab);

            Prefab.ApplyMaterialToChild("Cloth", "Paper");

            var parent = Prefab.GetChild("Tortillas");
            parent.ApplyMaterialToChildren("Wrap", "Bread - Inside");
            parent.ApplyMaterialToChildren("Char", "Well-done  Burger");
        }
    }
}
