namespace IngredientLib.Ingredient.Items
{
    public class Chicken : GenericItem<ChickenProvider>
    {
        public override string NameTag => "Chicken";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 2.5f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Result = GetCastedGDO<Item, CookedChicken>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("chicken", "Crab Cake - No Flour");
        }
    }
}
