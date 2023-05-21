using IngredientLib.Components;

namespace IngredientLib.Ingredient.Providers
{
    public class ButterProvider : GenericProvider
    {
        public override string NameTag => "Butter";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetCItemProvider(GetIngredient("Butter"), 0, 0, false, false, false, false, false, true, false),
            new CItemHolder(),
            new CPickupIgnoresHolder()
        };

        public override void Modify(Appliance gdo)
        {
            SetupFridge(Prefab);

            Prefab.GetChild("Fridge/Fridge2").ApplyMaterialToChildren("Shelf", "Plastic - Grey");
            Prefab.GetChild("Butter").ApplyMaterialToChildren("Butter", "Butter");

            Prefab.TryAddComponent<HoldPointContainer>().HoldPoint = Prefab.transform.Find("HoldPoint");
        }
    }
}
