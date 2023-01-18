using static KitchenData.Item;

namespace IngredientLib.Util
{
    public static class GDOHelper
    {
        public static void AddRecipe(this Item from, Item to, int process, float duration, bool isBad, bool requiresWrapper)
        {
            var itemProcess = new ItemProcess()
            {
                Process = GetGDO<Process>(process),
                Duration = duration,
                IsBad = isBad,
                RequiresWrapper = requiresWrapper,
                Result = to,
            };
            from.DerivedProcesses.Add(itemProcess);
        }
    }
}
