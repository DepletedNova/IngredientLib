namespace IngredientLib.Ingredient.Items
{
    public class WhippingCreamIngredient : GenericItem<WhippingCreamProvider>
    {
        public override string NameTag => "Whipping Cream";
        public override GameObject Prefab => GetPrefab("Whipping Cream Ingredient");
        public override bool IsSplitItem => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = .6f,
                Process = GetGDO<Process>(ProcessReferences.Knead),
                Result = GetCastedGDO<Item, WhippedCream>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Bowl", "Metal Dark");
            Prefab.ApplyMaterialToChild("Cream", "Coffee Cup");
        }
    }
}
