namespace IngredientLib.Ingredient.Items
{
    public class EggDough : GenericItem
    {
        public override string NameTag => "Egg Dough";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            var bowl = Prefab.GetChild("bowl");
            bowl.ApplyMaterialToChild("Cylinder.001", "Metal Dark");
            bowl.ApplyMaterialToChild("Cylinder", "Egg Dough");
        }
    }
}
