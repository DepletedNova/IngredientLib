namespace IngredientLib.Ingredient.Items
{
    public class Asparagus : GenericItem<AsparagusProvider>
    {
        public override string NameTag => "Asparagus";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = .6f,
                Process = GetGDO<Process>(ProcessReferences.Chop),
                Result = GetCastedGDO<Item, ChoppedAsparagus>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("gus", "Plant", "Bamboo Leaf");
        }
    }
}
