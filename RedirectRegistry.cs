namespace IngredientLib
{
    public static class RedirectRegistry
    {
        private static Dictionary<int, int> Redirects = new();

        public static void Redirect<T>(this T gdo, int redirection) where T : CustomGameDataObject
        {
            if (Redirects.ContainsKey(gdo.ID))
                return;
            Redirects.Add(gdo.ID, redirection);
        }

        public static void Redirect<T>(this T gdo, GameDataObject redirection) where T : CustomGameDataObject => gdo.Redirect(redirection.ID);
        
        public static void TryRedirect<T>(this T gdo, Action<T> action, GameData gameData) where T : GameDataObject
        {
            if (gdo == null) return;

            if (Redirects.TryGetValue(gdo.ID, out var redirect) && gameData.TryGet(redirect, out T outGDO))
            {
                action.Invoke(outGDO);
                Main.LogInfo($"[{typeof(T).Name}] Redirecting GDOs: {gdo.name} ({gdo.ID}) -> {outGDO.name} ({redirect})");
            }
        }

        public static bool IsRedirect(int id) => Redirects.ContainsValue(id);
        public static bool HasRedirect(int id) => Redirects.ContainsKey(id);
        public static bool TryGetRedirect(int id, out int redirect) => Redirects.TryGetValue(id, out redirect);
        public static int GetRedirect(int id) => Redirects[id];
    }
}
