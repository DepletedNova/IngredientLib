using Kitchen.Modules;

namespace IngredientLib.Menu
{
    public class PreferencesMenu<T> : KLMenu<T>
    {
        public PreferencesMenu(Transform container, ModuleList module_list) : base(container, module_list)
        {
        }

        public override void Setup(int player_id)
        {
            AddInfo("All changes will require a relaunch of the game.");

            AddLabel("Redirects");
            Add(new Option<bool>(new List<bool> { false, true }, Main.ApplyRedirects.Get(), new List<string> { "Off", "On" }
            )).OnChanged += delegate (object _, bool newVal)
            {
                Main.ApplyRedirects.Set(newVal);
                Main.PreferenceManager.Save();
            };
            AddInfo("Automatically replaces all modded ingredients in mods with their basegame variant.");

            AddButton(Localisation["MENU_BACK_SETTINGS"], delegate
            {
                RequestPreviousMenu();
            });
        }
    }
}
