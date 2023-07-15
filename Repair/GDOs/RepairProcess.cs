namespace IngredientLib.Repair.GDOs
{
    public class RepairProcess : CustomProcess
    {
        public override string UniqueNameID => "Repair Menu Process";
        public override List<(Locale, ProcessInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateProcessInfo("Repair", "<sprite name=\"repair_0\">"))
        };
        public override bool CanObfuscateProgress => false;
    }
}
