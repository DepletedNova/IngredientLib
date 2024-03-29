﻿namespace IngredientLib.Ingredient.Items
{
    public class ChoppedAvocado : GenericItem<AvocadoProvider>
    {
        public override string NameTag => "Chopped Avocado";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Result = GetCastedGDO<Item, MashedAvocado>(),
                Duration = 0.6f,
                Process = GetGDO<Process>(ProcessReferences.Chop)
            }
        };

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("avocado", "Avocado", "Avocado Inside");
        }
    }
}
