using Controllers;

namespace IngredientLib.Repair.Patches
{
    //[HarmonyPatch(typeof(PlayerPauseView))]
    static class PlayerPauseView_Patch
    {
        //[HarmonyPatch("PerformAction")]
        //[HarmonyPostfix]
        static void PerformAction_Postfix(PauseMenuAction action, PlayerPauseView __instance, int ___ActivePlayer)
        {
            if (action == Main.RepairAction)
            {
                InputSourceIdentifier.DefaultInputSource.MakeRequest(___ActivePlayer, Main.RepairRequest);
                __instance.Hide();
            }
        }
    }
}
