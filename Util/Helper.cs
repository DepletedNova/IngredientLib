using Kitchen;
using Unity.Entities;

namespace IngredientLib.Util
{
    public static class Helper
    {
        public static GameObject GetPrefab(string name)
        {
            return Main.bundle.LoadAsset<GameObject>(name);
        }

        // GDO
        public static T GetGDO<T>(int id) where T : GameDataObject
        {
            return GetExistingGDO(id) as T;
        }

        // Provider Util
        internal static List<Appliance.ApplianceProcesses> CreateCounterProcesses()
        {
            return new List<Appliance.ApplianceProcesses>()
            {
                new Appliance.ApplianceProcesses()
                {
                    Process = GetGDO<Process>(ProcessReferences.Chop),
                    Speed = 0.75f,
                    IsAutomatic = false,
                    Validity = ProcessValidity.Generic
                },
                new Appliance.ApplianceProcesses()
                {
                    Process = GetGDO<Process>(ProcessReferences.Knead),
                    Speed = 0.75f,
                    IsAutomatic = false,
                    Validity = ProcessValidity.Generic
                },
            };
        }
        internal static void SetupCounter(GameObject prefab)
        {
            GameObject parent = prefab.GetChild("Block/Counter2");
            var paintedWood = GetMaterialArray("Wood 4 - Painted");
            var defaultWood = GetMaterialArray("Wood - Default");
            parent.ApplyMaterialToChild("Counter", paintedWood);
            parent.ApplyMaterialToChild("Counter Doors", paintedWood);
            parent.ApplyMaterialToChild("Counter Surface", defaultWood);
            parent.ApplyMaterialToChild("Counter Top", defaultWood);
            parent.ApplyMaterialToChild("Handles", "Knob");
        }
        internal static void SetupCounterLimitedItem(GameObject counterPrefab, GameObject itemPrefab)
        {
            Transform holdTransform = GetChildObject(counterPrefab, "Block/HoldPoint").transform;

            counterPrefab.TryAddComponent<HoldPointContainer>().HoldPoint = holdTransform;
            
            var sourceView = counterPrefab.TryAddComponent<LimitedItemSourceView>();
            sourceView.HeldItemPosition = holdTransform;
            ReflectionUtils.GetField<LimitedItemSourceView>("Items").SetValue(sourceView, new List<GameObject>()
            {
                GetChildObject(counterPrefab, $"Block/HoldPoint/{itemPrefab.name}")
            });
        }
        internal static void SetupGenericCrates(GameObject prefab)
        {
            prefab.GetChild("GenericStorage").ApplyMaterialToChildren("Cube", "Wood - Default");
        }
        internal static void SetupFridge(GameObject prefab)
        {
            GameObject fridge = prefab.GetChild("Fridge");
            GameObject fridge2 = fridge.GetChild("Fridge2");

            prefab.TryAddComponent<ItemHolderView>();
            fridge.TryAddComponent<ItemHolderView>();

            var sourceView = fridge.TryAddComponent<ItemSourceView>();
            var quad = fridge.GetChild("Quad").GetComponent<MeshRenderer>();
            quad.materials = GetMaterialArray("Flat Image");
            ReflectionUtils.GetField<ItemSourceView>("Renderer").SetValue(sourceView, quad);
            ReflectionUtils.GetField<ItemSourceView>("Animator").SetValue(sourceView, fridge2.GetComponent<Animator>());

            var soundSource = fridge2.TryAddComponent<AnimationSoundSource>();
            soundSource.SoundList = new List<AudioClip>() { Main.bundle.LoadAsset<AudioClip>("Fridge_mixdown") };
            soundSource.Category = SoundCategory.Effects;
            soundSource.ShouldLoop = false;

            // Fridge Materials
            fridge2.ApplyMaterialToChild("Body", "Metal- Shiny", "Metal- Shiny", "Metal- Shiny");
            fridge2.ApplyMaterialToChild("Door", "Metal- Shiny", "Metal Dark", "Door Glass");
            fridge2.ApplyMaterialToChild("Divider", "Plastic - Dark Grey");
            fridge2.ApplyMaterialToChild("Wire", "Plastic - Blue");
        }
        internal static void SetupLocker(GameObject prefab)
        {
            // Components
            var lockerPrefab = prefab.GetChild("Locker");
            var lockerModel = lockerPrefab.GetChild("Locker");

            prefab.TryAddComponent<ItemHolderView>();
            lockerPrefab.TryAddComponent<ItemHolderView>();

            var sourceView = lockerPrefab.TryAddComponent<ItemSourceView>();
            var quad = lockerPrefab.GetChild("Quad").GetComponent<MeshRenderer>();
            quad.materials = GetMaterialArray("Flat Image");
            ReflectionUtils.GetField<ItemSourceView>("Renderer").SetValue(sourceView, quad);
            ReflectionUtils.GetField<ItemSourceView>("Animator").SetValue(sourceView, lockerModel.GetComponent<Animator>());

            var soundSource = lockerModel.TryAddComponent<AnimationSoundSource>();
            soundSource.SoundList = new List<AudioClip>() { Main.bundle.LoadAsset<AudioClip>("Fridge_mixdown") };
            soundSource.Category = SoundCategory.Effects;
            soundSource.ShouldLoop = false;

            // Models
            prefab.GetChild("Ice").ApplyMaterialToChildren("Ice", "Ice");
            lockerModel.ApplyMaterialToChild("Body", "Metal- Shiny", "Metal Dark");
            lockerModel.ApplyMaterialToChild("Wires", "Plastic - Red", "Plastic - Blue");
            lockerModel.ApplyMaterialToChild("Block", "Metal- Shiny", "Door Glass");
            lockerModel.ApplyMaterialToChild("Door", "Metal- Shiny", "Door Glass", "Metal Very Dark");
        }
        internal static void SetupStand(GameObject prefab)
        {
            // Component
            var holdPoint = prefab.TryAddComponent<HoldPointContainer>();
            holdPoint.HoldPoint = prefab.transform.Find("HoldPoint");

            // Model
            var stand = prefab.GetChild("Stand/Stand");
            stand.ApplyMaterialToChild("Body", "Wood 4 - Painted");
            stand.ApplyMaterialToChild("Doors", "Wood 4 - Painted");
            stand.ApplyMaterialToChild("Handles", "Metal - Brass");
            stand.ApplyMaterialToChild("Sides", "Wood - Default");
            stand.ApplyMaterialToChild("Top", "Wood - Default");
        }

    }
}
