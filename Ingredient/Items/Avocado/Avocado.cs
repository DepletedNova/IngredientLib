namespace IngredientLib.Ingredient.Items
{
    public class Avocado : GenericItem<AvocadoProvider>
    {
        public override string NameTag => "Avocado";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Result = GetCastedGDO<Item, ChoppedAvocado>(),
                Duration = 1.3f,
                Process = GetGDO<Process>(ProcessReferences.Chop)
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("avocado", "Avocado");
        }
    }
}
