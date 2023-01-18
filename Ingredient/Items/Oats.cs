namespace IngredientLib.Ingredient.Items
{
    public class Oats : GenericItem<OatsProvider>
    {
        public override string NameTag => "Oats";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("scoop", "Metal Black");
            Prefab.ApplyMaterialToChild("mound", "Oat Mound");
            Prefab.ApplyMaterialToChild("grain", "Oat Grain");
        }
    }
}
