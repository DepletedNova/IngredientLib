using KitchenLib.Colorblind;

namespace IngredientLib.Util.Custom
{
    public abstract class GenericItemGroup<T> : CustomItemGroup<T> where T : ItemGroupView
    {
        public abstract string NameTag { get; }

        public override string UniqueNameID => NameTag.ToLower();
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override GameObject Prefab => GetPrefab(NameTag);

        public override void Convert(GameData gameData, out GameDataObject gdo)
        {
            base.Convert(gameData, out gdo);

            AddIngredient(NameTag, gdo);
        }

        public override void OnRegister(ItemGroup gdo)
        {
            T component = Prefab.GetComponent<T>();
            if (component as AccessedItemGroupView != null)
                (component as AccessedItemGroupView).Setup(gdo);

            Modify(gdo);
        }

        public virtual void Modify(ItemGroup gdo) { }

        public abstract class AccessedItemGroupView : ItemGroupView
        {
            protected abstract List<ComponentGroup> groups { get; }
            protected virtual List<ColourBlindLabel> labels => new();

            private bool registered = false;
            public void Setup(GameDataObject gdo)
            {
                if (registered)
                    return;
                registered = true;

                ComponentGroups = groups;

                if (labels.Count > 0)
                {
                    ComponentLabels = labels;
                    ColorblindUtils.setColourBlindLabelObjectOnItemGroupView(this, ColorblindUtils.cloneColourBlindObjectAndAddToItem(gdo as Item));
                }
            }
        }
    }

    public abstract class GenericItemGroup<T, X> : GenericItemGroup<X> where T : GenericProvider where X : ItemGroupView
    {
        public override Appliance DedicatedProvider => GetCastedGDO<Appliance, T>();
    }

    public abstract class GenericItemGroup : GenericItemGroup<ItemGroupView> { }
}
