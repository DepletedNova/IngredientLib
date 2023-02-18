namespace IngredientLib.Ingredient.Items
{
    public class ChoppedLemon : GenericItem<LemonProvider>
    {
        public override string NameTag => "Chopped Lemon";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override string ColourBlindTag => "Le";

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 1.3f,
                Process = GetGDO<Process>(ProcessReferences.Knead),
                Result = GetCastedGDO<Item, LemonJuice>()
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("fruit", "Lemon", "Lemon Inner", "White Fruit");
        }
    }
}
