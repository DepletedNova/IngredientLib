using IngredientLib.Repair.Components;
using Unity.Entities;

namespace IngredientLib.Repair.Systems
{
    [UpdateAfter(typeof(InteractionGroup))]
    public class DisablePracticeOnCorrupted : NightSystem
    {
        protected override void OnUpdate()
        {
            if (!HasSingleton<SRepair>() || !HasSingleton<CRequestPracticeMode>())
                return;

            EntityManager.DestroyEntity(GetSingletonEntity<CRequestPracticeMode>());
        }
    }
}
