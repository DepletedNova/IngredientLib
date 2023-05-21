using IngredientLib.Components;
using Unity.Entities;

namespace IngredientLib.Patches
{
    [HarmonyPatch(typeof(TakeFromHolder))]
    internal class TakeFromHolder_Patch
    {
        [HarmonyPatch("CreateProposal")]
        [HarmonyPrefix]
        internal static bool CreateProposal_Prefix(Entity from, Entity to, bool is_drop, Entity interactor, ref TakeFromHolder __instance)
        {
            if (__instance.EntityManager.HasComponent<CPickupIgnoresHolder>(from))
                return false;
            return true;
        }
    }
}
