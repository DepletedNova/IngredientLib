namespace IngredientLib.Ingredient.Items
{
    public class BrownedMarshmallow : GenericItem
    {
        public override string NameTag => "Browned Marshmallow";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = .6f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Result = GetCastedGDO<Item, MeltedMarshmallow>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("marsh", "Batter - Cooked");
        }
    }
}
