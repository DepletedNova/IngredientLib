using IngredientLib.Repair.Components;
using IngredientLib.Repair.Systems;

namespace IngredientLib.Repair.Patches
{
    //[HarmonyPatch(typeof(MainMenu))]
    static class MainMenu_Patch
    {
        //[HarmonyPatch(nameof(MainMenu.Setup))]
        //[HarmonyPrefix]
        static void Setup_Prefix(MainMenu __instance)
        {
            if (!PatchController.StaticHasSingleton<SRepair>() || !GameInfo.IsPreparationTime || GameInfo.CurrentScene != SceneType.Kitchen)
                return;

            ReflectionUtils.GetMethod<Menu<PauseMenuAction>>("AddButton").Invoke(__instance, new object[]
            {
                GameData.Main.GlobalLocalisation["IL:Repair"],
                delegate(int i)
                {
                    ReflectionUtils.GetMethod<Menu<PauseMenuAction>>("RequestAction").Invoke(__instance, new object[]
                    {
                        Main.RepairAction
                    });
                },
                0, 1f, 0.2f
            });
        }
    }
}
