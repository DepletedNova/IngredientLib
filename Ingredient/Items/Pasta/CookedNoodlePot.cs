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

            Prefab.TryAddComponent<ItemGroupViewAccessed>().Setup();
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

        // Thanks to ZekNikZ for showing me this workaround.
        private class ItemGroupViewAccessed : ItemGroupView
        {
            internal void Setup()
            {
                var pasta = gameObject.GetChild("Pasta");
                var mac = gameObject.GetChild("Macaroni");
                ComponentGroups = new()
                {
                    new ComponentGroup()
                    {
                        GameObject = gameObject.GetChild("Water"),
                        Item = GetGDO<Item>(ItemReferences.Water)
                    },
                    new ComponentGroup()
                    {
                        GameObject = pasta.GetChild("BoxNoodle"),
                        Item = GetCastedGDO<Item, BoxNoodle>()
                    },
                    new ComponentGroup()
                    {
                        GameObject = pasta.GetChild("EggNoodle"),
                        Item = GetCastedGDO<Item, EggNoodle>()
                    },
                    new ComponentGroup()
                    {
                        GameObject = mac.GetChild("Mac"),
                        Item = GetCastedGDO<Item, Macaroni>()
                    },
                    new ComponentGroup()
                    {
                        Objects = new()
                        {
                            pasta.GetChild("CookedNoodle"),
                            gameObject.GetChild("Steam")
                        },
                        DrawAll = true,
                        Item = GetCastedGDO<Item, CookedNoodlePot>()
                    },
                    new ComponentGroup()
                    {
                        Objects = new()
                        {
                            mac.GetChild("CookedMac"),
                            gameObject.GetChild("Steam")
                        },
                        DrawAll = true,
                        Item = GetCastedGDO<Item, CookedMacaroniPot>()
                    }

                };
            }
        }

    }
}
