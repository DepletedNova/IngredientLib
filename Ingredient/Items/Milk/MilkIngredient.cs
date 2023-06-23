namespace IngredientLib.Ingredient.Items
{
    public class MilkIngredient : GenericItem<MilkProvider>
    {
        public override string NameTag => "Milk";
        public override bool IsSplitItem => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override GameObject Prefab => GetPrefab("Milk Ingredient");

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("cup", "Metal");
            Prefab.ApplyMaterialToChild("fill", "Coffee Cup");
        }
    }
}
