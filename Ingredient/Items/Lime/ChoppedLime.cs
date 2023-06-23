namespace IngredientLib.Ingredient.Items
{
    public class ChoppedLime : GenericItem<LimeProvider>
    {
        public override string NameTag => "Chopped Lime";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override string ColourBlindTag => "Li";

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 1.3f,
                Process = GetGDO<Process>(ProcessReferences.Knead),
                Result = GetCastedGDO<Item, LimeJuice>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("fruit", "Lime", "White Fruit");
        }
    }
}
