using IngredientLib.Repair.Components;
using Unity.Entities;

namespace IngredientLib.Repair.Systems
{
    public class RequestRepairCheck : GenericSystemBase, IModSystem
    {
        protected override void Initialise()
        {
            base.Initialise();
            RequireSingletonForUpdate<CSceneFirstFrame>();
        }

        protected override void OnUpdate()
        {
            if (Require(out SCurrentScene sScene) && sScene.Type == SceneType.Kitchen)
                EntityManager.CreateEntity(new ComponentType[] { typeof(SCheckForRepair) });
        }
    }
}
