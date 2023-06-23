namespace IngredientLib.Ingredient.Items
{
    public class ChoppedJalapeno: GenericItem<JalapenoProvider>
    {
        public override string NameTag => "Chopped Jalapeños";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override string ColourBlindTag => "Ja";

        public override void Modify(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("jalapeno", "Jalapeno", "White Fruit");
        }
    }
}
