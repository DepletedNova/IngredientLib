namespace IngredientLib.Util.Custom
{
    public abstract class GenericItem : CustomItem
    {
        public abstract string NameTag { get; }
        public virtual bool IsSplitItem { get; } = false;

        public override string UniqueNameID => $"{NameTag.ToLower()}{(IsSplitItem ? " ingredient" : "")}";
        public override GameObject Prefab => GetPrefab(NameTag);
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void Convert(GameData gameData, out GameDataObject gdo)
        {
            base.Convert(gameData, out gdo);

            if (IsSplitItem)
                AddSplitIngredient(NameTag, gdo);
            else
                AddIngredient(NameTag, gdo);
        }

        public override void OnRegister(GameDataObject gdo)
        {
            gdo.name = $"{(IsSplitItem ? "Split Ingredient" : "Ingredient")} - {NameTag}";
            
            Modify(gdo as Item);
        }

        public virtual void Modify(Item gdo) { }
    }

    public abstract class GenericItem<T> : GenericItem where T : GenericProvider
    {
        public override Appliance DedicatedProvider => GetCastedGDO<Appliance, T>();
    }
}
