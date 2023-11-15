namespace IngredientLib.Ingredient.Items
{
    public class Marshmallow : GenericItem<MarshmallowProvider>
    {
        public override string NameTag => "Marshmallow";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = .3f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Result = GetCastedGDO<Item, BrownedMarshmallow>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("marsh", "Plastic");
        }
    }
}
