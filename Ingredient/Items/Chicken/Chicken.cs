namespace IngredientLib.Ingredient.Items
{
    public class Chicken : GenericItem<ChickenProvider>
    {
        public override string NameTag => "Chicken";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override Appliance DedicatedProvider => GetCastedGDO<Appliance, ChickenProvider>();

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("chicken", "Raw Chicken");
        }
    }
}
