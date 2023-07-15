using IngredientLib.Repair.Components;
using IngredientLib.Repair.Systems;

namespace IngredientLib.Repair.Patches
{
    [HarmonyPatch(typeof(SStartDayWarnings))]
    internal static class SStartDayWarnings_Patch
    {
        internal static WarningLevel CorruptedSave = WarningLevel.Unknown;

        [HarmonyPatch("Primary", MethodType.Getter)]
        [HarmonyPostfix]
        static void Primary_Get_Postfix(ref StartDayWarning __result)
        {
            if (__result != StartDayWarning.Ready && __result != StartDayWarning.PlayersNotReady && __result != StartDayWarning.SellingRequiredAppliance)
                return;

            CorruptedSave = WarningLevel.Error.If(PatchController.StaticHasSingleton<SRepair>());
            if (CorruptedSave.IsActive())
                __result = Main.CorruptedSaveWarning;
        }
    }
}
