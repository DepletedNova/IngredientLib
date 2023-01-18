namespace IngredientLib.ApplianceUtil.Views
{
    public class DualProviderView : UpdatableObjectView<DualProviderView.ViewData>
    {
        protected override void UpdateData(ViewData data)
        {
            Animator.SetInteger(Index, data.ProviderIndex);
        }

        public Animator Animator;
        private static readonly int Index = Animator.StringToHash("Index");

        public class UpdateView : IncrementalViewSystemBase<ViewData>, IModSystem
        {
            protected override void OnUpdate()
            {
                using var views = Views.ToComponentDataArray<CLinkedView>(Allocator.Temp);
                using var components = Views.ToComponentDataArray<CDualProvider>(Allocator.Temp);

                for (var i = 0; i < views.Length; i++)
                {
                    var view = views[i];
                    var data = components[i];

                    SendUpdate(view, new ViewData
                    {
                        ProviderIndex = data.Current
                    }, MessageType.SpecificViewUpdate);
                }
            }

            protected override void Initialise()
            {
                base.Initialise();
                Views = GetEntityQuery(new QueryHelper().All(typeof(CLinkedView), typeof(CDualProvider)));
            }

            protected override void OnCreateForCompiler()
            {
                base.OnCreateForCompiler();
            }

            private EntityQuery Views;
        }

        [MessagePackObject]
        public struct ViewData : ISpecificViewData, IViewData.ICheckForChanges<ViewData>
        {
            [Key(0)] public int ProviderIndex;

            public IUpdatableObject GetRelevantSubview(IObjectView view) => view.GetSubView<DualProviderView>();

            public bool IsChangedFrom(ViewData check) => ProviderIndex != check.ProviderIndex;
        }
    }
}
