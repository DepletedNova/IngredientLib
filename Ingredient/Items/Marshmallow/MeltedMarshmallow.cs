namespace IngredientLib.Ingredient.Items
{
    public class MeltedMarshmallow : GenericItem
    {
        public override string NameTag => "Melted Marshmallow";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 1f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Result = GetGDO<Item>(ItemReferences.BurnedFood),
                IsBad = true
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("marsh", "Batter - Cooked");
        }
    }
}
