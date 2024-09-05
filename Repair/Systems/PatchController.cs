using Unity.Entities;

namespace IngredientLib.Repair.Systems
{
    public class PatchController : GenericSystemBase
    {
        static PatchController _instance;
        protected override void OnUpdate()
        {
            _instance = this;
        }

        public static bool StaticHasSingleton<T>() where T : struct, IComponentData => _instance?.HasSingleton<T>() ?? false;

        public static T StaticGetSingleton<T>() where T : struct, IComponentData => _instance.GetSingleton<T>();

        public static void StaticSet<T>() where T : struct, IComponentData => _instance.Set<T>();
    }
}
