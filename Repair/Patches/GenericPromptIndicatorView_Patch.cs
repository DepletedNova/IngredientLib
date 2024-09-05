using Controllers;
using IngredientLib.Repair.GDOs;
using Kitchen.Modules;
using TMPro;

namespace IngredientLib.Repair.Patches
{
    //[HarmonyPatch(typeof(GenericPromptIndicatorView))]
    static class GenericPromptIndicatorView_Patch
    {
        //[HarmonyPatch("UpdateData")]
        //[HarmonyPrefix]
        static bool UpdateData_Postfix(GenericPromptIndicatorView.ViewData data, ref TextMeshPro ___ActiveText, ref Animator ___Animator, ref InputPromptElement ___ActivePrompt)
        {
            if (data.OpenPromptFor == 0 || data.Message != RepairMenuTrigger.INDICATOR_MESSAGE)
                return true;

            var localisation = GameData.Main.GlobalLocalisation;
            ___ActiveText.text = localisation["IL:Repair"];
            ___ActivePrompt.SetButtonForUser(Controls.Interact2, data.OpenPromptFor);
            if (___Animator != null)
                ___Animator.Update(0f);

            return false;
        }
    }
}
