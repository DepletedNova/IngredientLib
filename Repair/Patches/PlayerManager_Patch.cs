using Controllers;
using IngredientLib.Repair.Components;
using IngredientLib.Repair.Systems;

namespace IngredientLib.Repair.Patches
{
    [HarmonyPatch(typeof(PlayerManager))]
    static class PlayerManager_Patch
    {
        [HarmonyPatch("HandleRequest")]
        [HarmonyPostfix]
        static void OnUpdate_Postfix(Player player, GameStateRequest request)
        {
            if (request == Main.RepairRequest)
            {
                PatchController.StaticSet<SRequestRepair>();
            }
        }
    }
}
