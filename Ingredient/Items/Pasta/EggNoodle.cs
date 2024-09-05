namespace IngredientLib.Ingredient.Items
{
    public class EggNoodle : GenericItem
    {
        public override string NameTag => "Egg Dough Pasta";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("pasta", "Egg Dough", "Sack");

            this.Redirect(ItemReferences.SpaghettiUncooked);
        }
    }
}
