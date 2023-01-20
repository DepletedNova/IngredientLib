namespace IngredientLib.Util.Interactions
{
    [UpdateBefore(typeof(ItemTransferGroup))]
    public class SwitchDualProvider : ItemInteractionSystem, IModSystem
    {
        protected override bool IsPossible(ref InteractionData data) => Require(data.Target, out itemProvider) && Require(data.Target, out dualProvider);

        protected override void Perform(ref InteractionData data)
        {
            dualProvider.Current = (dualProvider.Current + 1) % 2;
            int provide = dualProvider.Provide;
            SetComponent(data.Target, dualProvider);
            itemProvider.SetAsItem(provide);
            SetComponent(data.Target, itemProvider);
        }

        private CDualProvider dualProvider;
        private CItemProvider itemProvider;
    }

    public class DualProviderRegistry : GenericSystemBase, IModSystem
    {
        private EntityQuery Query;
        protected override void Initialise()
        {
            base.Initialise();
            Query = GetEntityQuery(new QueryHelper().All(typeof(CAppliance), typeof(CItemProvider)).None(typeof(CDualProvider)));
        }

        protected override void OnUpdate()
        {
            using var appliances = Query.ToEntityArray(Allocator.TempJob);
            foreach (var appliance in appliances)
            {
                int id = EntityManager.GetComponentData<CAppliance>(appliance).ID;
                foreach (Property<CDualProvider> property in properties)
                {
                    if (property.Provider.GameDataObject.ID != id)
                        continue;

                    EntityManager.AddComponentData(appliance, property.Value);
                }
            }
        }
        public static void AddProvider(GenericProvider provider, CDualProvider property) => properties.Add(new Property<CDualProvider>() { Value = property, Provider = provider });
        private static List<Property<CDualProvider>> properties = new List<Property<CDualProvider>>();

        private struct Property<C> where C : struct, IComponentData
        {
            public GenericProvider Provider { get; set; }
            public C Value { get; set; }
        }
    }
}
