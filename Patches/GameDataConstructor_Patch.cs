using static KitchenData.Item;
using Priority = HarmonyLib.Priority;

namespace IngredientLib.Patches
{
    [HarmonyPatch(typeof(GameDataConstructor), "BuildGameData")]
    class GameDataConstructor_Patch
    {
        private static Dictionary<int, List<ItemProcess>> cachedProcesses = new Dictionary<int, List<ItemProcess>>();

        static void Prefix()
        {
            if (!cachedProcesses.IsNullOrEmpty()) return;

            cachedProcesses = new Dictionary<int, List<ItemProcess>>();
            foreach (var cgdo in CustomGDO.GDOs.Values)
            {
                if (cachedProcesses.ContainsKey(cgdo.ID) || !cgdo.IsSubclassOfGeneric(typeof(CustomItem<>)))
                    continue;

                var cItem = cgdo as CustomItem<Item>;
                if (cItem == null) continue;

                cachedProcesses.Add(cItem.ID, cItem.Processes == null ? new List<ItemProcess>() : new List<ItemProcess>(cItem.Processes));
            }
        }

        [HarmonyPriority(Priority.Low), HarmonyAfter("kitchenlib")]
        static void Postfix(GameDataConstructor __instance, GameData __result)
        {
            // Get the differences in item processes applied during the BuildGameData and apply them to the appropriate items
            foreach (var cgdo in CustomGDO.GDOs.Values)
            {
                if (cgdo == null || !RedirectRegistry.HasRedirect(cgdo.ID) || !cachedProcesses.ContainsKey(cgdo.ID)) 
                    continue;

                var cgdoItem = cgdo.GameDataObject as Item;
                if (cgdoItem == null || cgdoItem.DerivedProcesses.IsNullOrEmpty() || !RedirectRegistry.TryGetRedirect(cgdo.ID, out int itemID)) 
                    continue;

                var processDifference = new List<ItemProcess>(cgdoItem.DerivedProcesses);
                var cachedProcess = cachedProcesses[cgdo.ID];
                processDifference.RemoveAll(x => x.Result == null || RedirectRegistry.HasRedirect(x.Result.ID) || cachedProcess.Any(y => y.Result != null && y.Result.ID == x.Result.ID));

                var item = __result.Get(itemID) as Item;
                if (item == null || processDifference.IsNullOrEmpty()) 
                    continue;

                Main.LogInfo("Updating processes for: " + cgdo.GetType().Name);

                if (item.DerivedProcesses.IsNullOrEmpty()) item.DerivedProcesses = new List<ItemProcess>();
                item.DerivedProcesses.AddRange(processDifference);
                typeof(Item).GetField("Processes", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(item, item.DerivedProcesses);
            }

            __result.ProcessesView.Initialise(__result);
        }
    }
}
