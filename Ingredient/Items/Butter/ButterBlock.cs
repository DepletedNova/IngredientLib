namespace IngredientLib.Ingredient.Items
{
    public class ButterBlock : GenericItem<ButterProvider>
    {
        public override string NameTag => "Butter";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override Item SplitSubItem => GetCastedGDO<Item, Butter>();
        public override int SplitCount => 6;
        public override float SplitSpeed => 2.5f;

        public override void Modify(Item gdo)
        {
            gdo.SplitDepletedItems.Add(GetCastedGDO<Item, Butter>());

            Prefab.ApplyMaterialToChildren("butterslice", "Butter");

            var splittable = Prefab.TryAddComponent<ObjectsSplittableView>();
            var items = new List<GameObject>();
            for (int i = 1; i < Prefab.GetChildCount(); i++)
                items.Add(Prefab.GetChild(i).gameObject);
            ReflectionUtils.GetField<ObjectsSplittableView>("Objects").SetValue(splittable, items);
        }
    }
}
