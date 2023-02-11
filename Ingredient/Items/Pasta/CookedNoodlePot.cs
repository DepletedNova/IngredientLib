namespace IngredientLib.Ingredient.Items
{
    public class CookedNoodlePot : GenericItem
    {
        public override string NameTag => "Cooked Potted Pasta";
        public override GameObject Prefab => GetPrefab("Potted Pasta");
        public override Item DisposesTo => GetGDO<Item>(ItemReferences.Pot);
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;

        public override void Convert(GameData gameData, out GameDataObject gdo)
        {
            base.Convert(gameData, out gdo);
        }

        public override void AttachDependentProperties(GameData gameData, GameDataObject gdo)
        {
            base.AttachDependentProperties(gameData, gdo);
        }

        public override void Modify(Item gdo)
        {
            var pot = Prefab.GetChild("Pot");
            var pasta = Prefab.GetChild("Pasta");
            var mac = Prefab.GetChild("Macaroni");

            // Visuals
            pot.ApplyMaterialToChild("Cylinder", "Metal");
            pot.ApplyMaterialToChild("Cylinder.003", "Metal Dark");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
            Prefab.ApplyMaterialToChild("Water", "Water");

            pasta.ApplyMaterialToChild("EggNoodle", "Sack");
            pasta.ApplyMaterialToChild("BoxNoodle", "Sack");
            pasta.ApplyMaterialToChild("CookedNoodle", "Raw Pastry");

            mac.GetChild("Mac").ApplyMaterialToChildren("roni", "Sack");
            mac.GetChild("CookedMac").ApplyMaterialToChildren("roni", "Raw Pastry");
        }
    }
}
