namespace IngredientLib.Ingredient.Items
{
    public class Macaroni : GenericItem<MacaroniProvider>
    {
        public override string NameTag => "Macaroni";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Scoop", "Metal");
            Prefab.ApplyMaterialToChild("Layer", "Sack");
            Prefab.GetChild("Macaroni").ApplyMaterialToChildren("Mac", "Sack");
        }
    }
}
