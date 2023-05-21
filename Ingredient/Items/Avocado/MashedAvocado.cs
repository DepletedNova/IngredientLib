namespace IngredientLib.Ingredient.Items
{
    public class MashedAvocado : GenericItem<AvocadoProvider>
    {
        public override string NameTag => "Mashed Avocado";

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChild("avocado", "Avocado Mash");
            Prefab.ApplyMaterialToChild("bowl", "Wood 1 - Dim");
        }
    }
}
