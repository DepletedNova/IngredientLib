namespace IngredientLib.Ingredient.Items
{
    public class ButterBlock : GenericItem<ButterProvider>
    {
        public override string NameTag => "Butter";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override GameObject Prefab => GetPrefab("Butter Block");

        public override Item SplitSubItem => GetCastedGDO<Item, Butter>();
        public override List<Item> SplitDepletedItems => new() { GetCastedGDO<Item, Butter>() };
        public override int SplitCount => 6;
        public override float SplitSpeed => 1.5f;

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("ButterSlice", "Butter");
            Prefab.ApplyMaterialToChild("Tray", "Coffee Cup");

            var splittable = Prefab.TryAddComponent<ObjectsSplittableView>();
            var items = new List<GameObject>();
            for (int i = 1; i < Prefab.GetChildCount() - 1; i++)
                items.Add(Prefab.GetChild(i).gameObject);
            ReflectionUtils.GetField<ObjectsSplittableView>("Objects").SetValue(splittable, items);
        }
    }
}
