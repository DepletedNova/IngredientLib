namespace IngredientLib.Ingredient.Items
{
    public class ChoppedAsparagus : GenericItem
    {
        public override string NameTag => "Chopped Asparagus";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("gus", "Plant", "Bamboo Leaf");
        }
    }
}
