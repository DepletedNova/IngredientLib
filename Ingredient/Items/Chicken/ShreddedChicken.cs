namespace IngredientLib.Ingredient.Items
{
    public class ShreddedChicken : GenericItem<ChickenProvider>
    {
        public override string NameTag => "Shredded Chicken";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("Shaving", "Cooked Chicken");
        }
    }
}
