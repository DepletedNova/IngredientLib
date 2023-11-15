using Unity.Entities;

namespace IngredientLib.Ingredient.Providers
{
    public class MarshmallowProvider : GenericProvider
    {
        public override string NameTag => "Marshmallow";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Marshmallow"))
        };

        public override void OnRegister(Appliance gdo)
        {
            Prefab.ApplyMaterialToChild("Table", "Wood - Default");
            Prefab.ApplyMaterialToChild("Cloth", "Cloth - Cheap");

            Prefab.ApplyMaterialToChildren("Marsh", "Plastic");
            Prefab.ApplyMaterialToChild("Stripe", "Plastic - Red", "Plastic - Green", "Plastic - Red", "Plastic - Orange");
        }
    }
}
