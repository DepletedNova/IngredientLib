namespace IngredientLib.Ingredient.Items
{
    public class Caramel : GenericItem
    {
        public override string NameTag => "Caramel";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            var bowl = Prefab.GetChild("bowl");
            bowl.ApplyMaterialToChild("Cylinder.001", "Metal Dark");
            bowl.ApplyMaterialToChild("Cylinder", "Caramel");
        }
    }
}
