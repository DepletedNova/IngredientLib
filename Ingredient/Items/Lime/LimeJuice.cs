﻿namespace IngredientLib.Ingredient.Items
{
    public class LimeJuice : GenericItem<LimeProvider>
    {
        public override string NameTag => "Lime Juice";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override string ColourBlindTag => "Li";

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("cup", "Metal");
            Prefab.ApplyMaterialToChild("fill", "Lime Juice");
        }
    }
}
