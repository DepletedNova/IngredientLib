namespace IngredientLib
{
    public static class SourceReferences
    {
        public static int Vinegar = -1364500983;
        public static int Chocolate = -1641853284;
        public static int Butter = 1668680768;
        public static int Milk = -182839971;
        public static int Banana = -1330174463;
        public static int Pepper = -68381129;
        public static int Lemon = 1969556125;
        public static int Lime = -1028296404;
        public static int Oats = 2094446100;
        public static int Pork = -97167681;
        public static int Chicken = 1525634059;
        public static int Drumstick = -230962390;
        public static int Honey = -2139088047;
        public static int WhippingCream = -1225413052;
        public static int Ice = 0;
    }

    public static class IngredientReferences
    {
        public static int Vinegar = 2060900032;
        public static int Butter = 154863149;
        public static int Milk = -1639512204;
        public static int Chocolate = -1765387269;
        public static int ChoppedChocolate = 1224024856;
        public static int ShavedChocolate = 1860193513;
        public static int ChocolateSauce = 1130329110;
        public static int Ganache = 469143321;
        public static int Banana = 1109387987;
        public static int PeeledBanana = 2012696979;
        public static int ChoppedBanana = 2044745994;
        public static int BananaPeel = 169506130;
        public static int Pepper = -1305651294;
        public static int ChoppedPepper = -1616869800;
        public static int Lemon = -1536459651;
        public static int ChoppedLemon = 1909019037;
        public static int LemonJuice = 828184747;
        public static int Lime = -1225823422;
        public static int ChoppedLime = -2012096263;
        public static int LimeJuice = 122648805;
        public static int Oats = 878084900;
        public static int Pork = -1059651045;
        public static int Porkchop = 1754393176;
        public static int BurnedPorkchop = -641273484;
        public static int ChoppedPork = 1107972170;
        public static int Bacon = -2094235032;
        public static int Chicken = 510972621;
        public static int CookedChicken = -675527872;
        public static int ShreddedChicken = -2038497941;
        public static int Drumstick = -1737864771;
        public static int CookedDrumstick = -248036484;
        public static int BonelessDrumstick = -564281071;
        public static int BonelessCookedDrumstick = 1732228185;
        public static int DrumstickBone = 773434563;
        public static int Honey = 1135326563;
        public static int WhippingCream = 1619896222;
        public static int Caramel = 0;
        public static int Batter = 0;
        public static int UnmixedEggDough = 0;
        public static int EggDough = 0;
        public static int EggNoodles = 0;
        public static int Ice = 0;
    }

    public static class SplitIngredientReferences
    {
        public static int Milk = 1315189495;
        public static int Butter = 1780648115;
        public static int Vinegar = 1709456108;
        public static int Honey = 779873085;
        public static int WhippingCream = 1319735349;
    }

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
