using IngredientLib.Repair.Components;
using IngredientLib.Repair.Systems;

namespace IngredientLib.Repair.Patches
{
    /*[HarmonyPatch(typeof(PopupUtilities))]
    static class PopupUtilities_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(PopupUtilities.RequestManagedPopup))]
        static bool RequestManagedPopup_Prefix(PopupType type)
        {
            if (PatchController.StaticHasSingleton<SRepair>() && type == PopupType.EnterPracticeMode)
                return false;
            return true;
        }
    }*/
}
