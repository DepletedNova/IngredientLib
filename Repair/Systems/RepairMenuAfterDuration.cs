using IngredientLib.Repair.Components;
using Kitchen;
using KitchenData;
using Unity.Collections;
using Unity.Entities;
using static KitchenData.Dish;
using static Unity.Collections.AllocatorManager;

namespace IngredientLib.Repair.Systems
{
    public class RepairMenuAfterDuration : GameSystemBase, IModSystem
    {
        private EntityQuery RepairTriggers;

        private EntityQuery Appliances;
        private EntityQuery Unlocks;
        private EntityQuery Menus;
        private EntityQuery Ingredients;
        private EntityQuery Extras;
        private EntityQuery Blockers;

        protected override void Initialise()
        {
            base.Initialise();
            RepairTriggers = GetEntityQuery(new QueryHelper()
                .All(typeof(CAppliance), typeof(CRepairAfterDuration), typeof(CTakesDuration)));

            Appliances = GetEntityQuery(new QueryHelper()
                .Any(typeof(CAppliance), typeof(CCreateAppliance), typeof(CLetterAppliance)));
            Unlocks = GetEntityQuery(new QueryHelper()
                .All(typeof(CProgressionUnlock)));
            Menus = GetEntityQuery(new QueryHelper()
                .All(typeof(CMenuItem)));
            Ingredients = GetEntityQuery(new QueryHelper()
                .All(typeof(CAvailableIngredient))
                .None(typeof(CMenuItem)));
            Extras = GetEntityQuery(new QueryHelper()
                .All(typeof(CPossibleExtra)));
            Blockers = GetEntityQuery(new QueryHelper()
                .All(typeof(CBlockedIngredient)));
        }

        protected override void OnUpdate()
        {
            var entities = RepairTriggers.ToEntityArray(Allocator.Temp);
            foreach (var trigger in entities)
            {
                var cDuration = GetComponent<CTakesDuration>(trigger);
                if (cDuration.Remaining > 0f || !cDuration.Active)
                    continue;

                Set<SRequestRepair>();
            }
            entities.Dispose();
        }
    }
}
