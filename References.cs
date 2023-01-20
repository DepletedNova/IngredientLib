namespace IngredientLib
{
    public static class References
    {
        public static Dictionary<string, int> ingredientReferences = new Dictionary<string, int>();
        public static Dictionary<string, int> splitIngredientReferences = new Dictionary<string, int>();
        public static Dictionary<string, int> providerReferences = new Dictionary<string, int>();

        public static int GetIngredient(string name) => ingredientReferences[name.ToLower()];
        public static int GetSplitIngredient(string name) => splitIngredientReferences[name.ToLower()] | 0;
        public static int GetProvider(string name) => providerReferences[name.ToLower()];

        internal static void AddIngredient<T>(string name, T item) where T : GameDataObject
        {
            if (ingredientReferences.ContainsKey(name.ToLower()))
                return;
            ingredientReferences.Add(name.ToLower(), item.ID);
        }
        internal static void AddSplitIngredient<T>(string name, T item) where T : GameDataObject
        {
            if (splitIngredientReferences.ContainsKey(name.ToLower()))
                return;
            splitIngredientReferences.Add(name.ToLower(), item.ID);
        }
        internal static void AddProvider<T>(string name, T item) where T : GameDataObject
        {
            if (providerReferences.ContainsKey(name.ToLower()))
                return;
            providerReferences.Add(name.ToLower(), item.ID);
        }
    }
}
