namespace IngredientLib.Ingredient.Items
{
    public class Caramel : GenericItem
    {
        public override string NameTag => "Caramel";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override Appliance DedicatedProvider => GetGDO<Appliance>(ApplianceReferences.SourceSugar);

        public override string ColourBlindTag => "Ca";

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 15f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                IsBad = true,
                Result = GetGDO<Item>(ItemReferences.BurnedFood)
            }
        };

        public override void Modify(Item gdo)
        {
            var bowl = Prefab.GetChild("bowl");
            bowl.ApplyMaterialToChild("Cylinder.001", "Metal Dark");
            bowl.ApplyMaterialToChild("Cylinder", "Caramel");
        }
    }
}
