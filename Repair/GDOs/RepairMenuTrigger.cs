using IngredientLib.Repair.Components;

namespace IngredientLib.Repair.GDOs
{
    public class RepairMenuTrigger : CustomAppliance
    {
        public static InputIndicatorMessage INDICATOR_MESSAGE = (InputIndicatorMessage)VariousUtils.GetID("Repair Menu Trigger Indicator");
        public static int ApplianceID { get; private set; }

        public override string UniqueNameID => "Repair Menu Trigger";

        public override GameObject Prefab => GetPrefab("Repair Menu Trigger");

        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Repair Menu Trigger", "", new(), new()))
        };

        public override List<IApplianceProperty> Properties => new()
        {
            new CImmovable(),
            new CFixedRotation(),
            new CDestroyApplianceAtDay { HideBin = true },
            new CTakesDuration
            {
                Total = 2,
                Manual = true,
                Mode = InteractionMode.Appliances,
            },
            new CDisplayDuration()
            {
                Process = GetCustomGameDataObject<RepairProcess>().GameDataObject.ID
            },
            new CRepairAfterDuration(),
            new CRequiresGenericInputIndicator()
            {
                Message = INDICATOR_MESSAGE
            }
        };

        public override void AttachDependentProperties(GameData gameData, GameDataObject gdo)
        {
            base.AttachDependentProperties(gameData, gdo);
            ApplianceID = gdo.ID;
        }

        public override void SetupPrefab(GameObject prefab)
        {
            var triggerPrefab = GetGDO<Appliance>(ApplianceReferences.PracticeModeTrigger).Prefab;

            var floorLabel = UnityEngine.Object.Instantiate(triggerPrefab.GetChild("Floor Label"));
            floorLabel.transform.SetParent(prefab.transform, false);

            var localisation = floorLabel.GetChild("Label").GetComponent<AutoGlobalLocal>();
            localisation.Text = "IL:Repair";

            var wrench = prefab.GetChild("Container/Wrench");
            wrench.ApplyMaterial("Glowing Blue Soft");

            var spin = wrench.TryAddComponent<Spin>();
            spin.SpinDuringGameplay = true;
            spin.SpinRate = -15f;
        }
    }
}
