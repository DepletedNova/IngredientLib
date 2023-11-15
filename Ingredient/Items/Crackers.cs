namespace IngredientLib.Ingredient.Items
{
    public class Crackers : GenericItem<CrackersProvider>
    {
        public override string NameTag => "Crackers";

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("cracker", "Raw Pastry");
        }
    }
}
