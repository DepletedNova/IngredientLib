using IngredientLib.Components;

namespace IngredientLib.Ingredient.Items
{
    public class Milk : GenericItem<MilkProvider>
    {
        public override string NameTag => "Milk";
        public override bool IsIndisposable => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;

        public override Item SplitSubItem => GetCastedGDO<Item, MilkIngredient>();
        public override int SplitCount => 999;
        public override float SplitSpeed => 2.0f;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("carton", "Plastic - Blue", "Plastic - White");
        }
    }
}
