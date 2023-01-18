namespace IngredientLib.ApplianceUtil.Properties
{
    public struct CDualProvider : IApplianceProperty, IAttachableProperty, IComponentData, IModComponent
    {
        public int Provide { get => Current == 1 ? Provide2 : Provide1; }

        public int Current;
        public int Provide1;
        public int Provide2;
    }
}
