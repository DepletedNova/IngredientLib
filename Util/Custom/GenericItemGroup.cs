namespace IngredientLib.Util.Custom
{
    public abstract class GenericItemGroup : CustomItemGroup
    {
        public abstract string NameTag { get; }
        public abstract List<ItemSet> ItemSets { get; }

        public override string UniqueNameID => NameTag.ToLower();
        public override GameObject Prefab => GetPrefab(NameTag);

        public override void Convert(GameData gameData, out GameDataObject gdo)
        {
            base.Convert(gameData, out gdo);

            AddIngredient(NameTag, gdo);
        }

        public override void OnRegister(GameDataObject gdo)
        {
            gdo.name = $"Ingredient - {NameTag}";

            ReflectionUtils.GetField<ItemGroup>("Sets").SetValue(gdo as ItemGroup, ItemSets);

            Modify(gdo as ItemGroup);
        }

        public virtual void Modify(ItemGroup gdo) { }
    }

    public abstract class GenericItemGroup<T> : GenericItemGroup where T : GenericProvider
    {
        public override Appliance DedicatedProvider => GetCastedGDO<Appliance, T>();
    }
}
