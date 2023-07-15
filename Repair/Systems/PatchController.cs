using Unity.Entities;

namespace IngredientLib.Repair.Systems
{
    public class PatchController : GenericSystemBase, IModSystem
    {
        static PatchController _instance;
        protected override void OnUpdate()
        {
            _instance = this;
        }

        public static bool StaticHasSingleton<T>() where T : struct, IComponentData => _instance?.HasSingleton<T>() ?? false;
    }
}
