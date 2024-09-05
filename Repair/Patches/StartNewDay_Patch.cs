namespace IngredientLib.Repair.Patches
{
    //[HarmonyPatch(typeof(StartNewDay))]
    static class StartNewDay_Patch
    {
        //[HarmonyPatch("OnUpdate")]
        //[HarmonyPrefix]
        static bool OnUpdate_Prefix()
        {
            return !SStartDayWarnings_Patch.CorruptedSave.IsBlocking();
        }
    }
}
