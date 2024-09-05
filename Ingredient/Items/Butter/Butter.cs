namespace IngredientLib.Ingredient.Items
{
    public class Butter : GenericItem<ButterProvider>
    {
        public override string NameTag => "Butter";
        public override bool IsSplitItem => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Slice", "Butter");

            this.Redirect(ItemReferences.Butter);
        }
    }
}
