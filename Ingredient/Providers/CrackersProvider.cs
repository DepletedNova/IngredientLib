using Unity.Entities;

namespace IngredientLib.Ingredient.Providers
{
    public class CrackersProvider : GenericProvider
    {
        public override string NameTag => "Crackers";
        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetUnlimitedCItemProvider(GetIngredient("Crackers"))
        };

        public override void OnRegister(Appliance gdo)
        {
            Prefab.ApplyMaterialToChild("Table", "Wood - Default");
            Prefab.ApplyMaterialToChild("Cloth", "Cloth - Cheap");

            Prefab.ApplyMaterialToChild("Box", "Plastic - Blue", "Plastic");
            Prefab.ApplyMaterialToChildren("Cracker", "Raw Pastry");
        }
    }
}
