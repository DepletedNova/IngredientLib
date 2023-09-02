using KitchenLib.DevUI;
using MessagePack.Formatters;
using System.Security.Permissions;
using Unity.Collections;
using Unity.Entities;

namespace IngredientLib.Menu
{
    public class ComponentMenu : BaseUI
    {
        public ComponentMenu() => ButtonName = "Components";

        private const float windowWidth = 775f;
        protected Texture2D Background;

        internal static GUIStyle LabelCentreStyle;
        internal static GUIStyle LabelMiddleCentreStyle;
        internal static GUIStyle LabelLeftStyle;
        internal static GUIStyle ToggleCentreStyle;

        internal static bool ShowOrders = true;
        internal static bool ShowItems = true;

        private static Vector2 scrollPosition = new Vector2(0, 0);

        private List<ListedItem> listed = new();

        public override void OnInit()
        {
            Background = new Texture2D(64, 64);
            Color backgroundColour = new Color(0.2f, 0.2f, 0.2f, 0.6f);
            for (int x = 0; x < 64; x++)
                for (int y = 0; y < 64; y++)
                    Background.SetPixel(x, y, backgroundColour);
            Background.Apply();
        }

        public override void Setup()
        {
            SetupStyles();

            // Filter
            GUILayout.BeginArea(new Rect(10f, 0f, windowWidth, 55f));
            GUI.DrawTexture(new Rect(0f, 0f, windowWidth, 55f), Background, ScaleMode.StretchToFill);

            GUILayout.Label("Filter", LabelLeftStyle);

            GUILayout.BeginHorizontal(LabelMiddleCentreStyle);
            ShowOrders = GUILayout.Toggle(ShowOrders, "Show Orders", ToggleCentreStyle);
            ShowItems = GUILayout.Toggle(ShowItems, "Show ItemGroups", ToggleCentreStyle);
            GUILayout.EndHorizontal();

            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(10f, 65f, windowWidth, 25f));
            if (GUILayout.Button("Retrieve items", GUILayout.Width(windowWidth)))
            {
                listed = ComponentDebugSystem.instance.GetListedItems();
            }
            GUILayout.EndArea();

            // Selection
            GUILayout.BeginArea(new Rect(10f, 90f, windowWidth, 600f));
            GUI.DrawTexture(new Rect(0f, 0f, windowWidth, 600f), Background, ScaleMode.StretchToFill);

            if (listed.IsNullOrEmpty())
            {
                GUILayout.Label("Could not find any applicable items!", LabelMiddleCentreStyle);
                GUILayout.EndArea();
                return;
            }

            GUILayout.Label($"Listed Items ({listed.Count})", LabelCentreStyle);
                

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar);

            foreach (var item in listed)
            {
                item.Draw();
            }

            GUILayout.EndScrollView();

            GUILayout.EndArea();
        }

        private void SetupStyles()
        {
            if (LabelCentreStyle == null)
            {
                LabelCentreStyle = new GUIStyle(GUI.skin.label);
                LabelCentreStyle.alignment = TextAnchor.MiddleCenter;
                LabelCentreStyle.stretchWidth = true;
            }

            if (LabelMiddleCentreStyle == null)
            {
                LabelMiddleCentreStyle = new GUIStyle(GUI.skin.label);
                LabelMiddleCentreStyle.alignment = TextAnchor.MiddleCenter;
                LabelMiddleCentreStyle.stretchWidth = true;
                LabelMiddleCentreStyle.stretchHeight = true;
            }

            if (LabelLeftStyle == null)
            {
                LabelLeftStyle = new GUIStyle(GUI.skin.label);
                LabelLeftStyle.alignment = TextAnchor.MiddleLeft;
                LabelLeftStyle.padding.left = 5;
                LabelLeftStyle.stretchWidth = true;
            }

            if (ToggleCentreStyle == null)
            {
                ToggleCentreStyle = new GUIStyle(GUI.skin.toggle);
                ToggleCentreStyle.alignment = TextAnchor.MiddleCenter;
                ToggleCentreStyle.stretchWidth = true;
            }
        }
    }

    internal class ComponentDebugSystem : GenericSystemBase, IModSystem
    {
        public static ComponentDebugSystem instance;

        private EntityQuery ItemQuery;
        private EntityQuery OrderQuery;

        protected override void Initialise()
        {
            base.Initialise();
            ItemQuery = GetEntityQuery(new ComponentType[] { typeof(CItem), typeof(CHeldBy) });
            OrderQuery = GetEntityQuery(new ComponentType[] { typeof(CWaitingForItem), typeof(CGroupMealPhase), typeof(CAtTable), typeof(CGroupMember) });
        }

        protected override void OnUpdate()
        {
            if (instance != this)
                instance = this;
        }

        public List<ListedItem> GetListedItems()
        {
            List<ListedItem> result = new();

            if (ComponentMenu.ShowItems)
            {
                var cItems = ItemQuery.ToComponentDataArray<CItem>(Allocator.Temp);
                foreach (var item in cItems)
                {
                    if (!item.IsGroup || item.Category != ItemCategory.Generic)
                        continue;

                    result.Add(new ListedItemGroup(item));
                }
                cItems.Dispose();
            }

            if (ComponentMenu.ShowOrders)
            {
                var customerGroups = OrderQuery.ToEntityArray(Allocator.Temp);
                for (int i = 0; i < customerGroups.Length; i++)
                {
                    var group = customerGroups[i];

                    var phase = GetComponent<CGroupMealPhase>(group).Phase;

                    var listedOrder = new ListedOrder(i, phase);

                    var awaitingItemBuffer = GetBuffer<CWaitingForItem>(group);
                    foreach (var awaitingItem in awaitingItemBuffer)
                    {
                        if (!Require(awaitingItem.Item, out CItem cItem))
                            continue;

                        listedOrder.AddItem(awaitingItem.MemberIndex, cItem);
                    }

                    result.Add(listedOrder);
                }
                customerGroups.Dispose();
            }

            return result;
        }
    }

    internal class ListedItemGroup : ListedItem
    {
        private List<string> Components = new();
        private bool IsPartial;
        private bool IsTransient;

        public ListedItemGroup(CItem cItem)
        {
            Name = $"({cItem.ID}) - \"{(GameData.Main.TryGet(cItem.ID, out Item item) ? item.name : "NULL")}\"";
            IsPartial = cItem.IsPartial;
            IsTransient = cItem.IsTransient;

            foreach (var component in cItem.Items)
                Components.Add($"({component}) - \"{(GameData.Main.TryGet(component, out Item compItem) ? compItem.name : "NULL")}\"");
        }

        protected override void DrawExpanded()
        {
            GUILayout.Label($"Partial: {IsPartial}");
            GUILayout.Label($"Transient: {IsTransient}");

            GUILayout.Label("Components:");
            GUILayout.BeginHorizontal();
            GUILayout.Space(IndentUnit);
            GUILayout.BeginVertical();
            foreach (var comp in Components)
            {
                GUILayout.Label(comp);
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
    }

    internal class ListedOrder : ListedItem
    {
        private Dictionary<int, ListedCustomer> Customers = new();

        public void AddItem(int index, CItem cItem)
        {
            if (!Customers.ContainsKey(index))
                Customers.Add(index, new ListedCustomer(index));

            Customers[index].ListedItems.Add(new ListedItemGroup(cItem));
        }

        public ListedOrder(int index, MenuPhase phase)
        {
            Name = $"({index}) - {phase} Phase";
        }

        private class ListedCustomer : ListedItem
        {
            public List<ListedItemGroup> ListedItems = new();

            public ListedCustomer(int index)
            {
                Name = $"Customer {index}";
            }

            protected override void DrawExpanded()
            {
                foreach (var listed in ListedItems)
                {
                    listed.Draw();
                }
            }
        }

        protected override void DrawExpanded()
        {
            foreach (var listed in Customers.Values)
            {
                listed.Draw();
            }
        }
    }

    internal abstract class ListedItem
    {
        protected string Name;
        private bool IsExpanded;

        protected const int IndentUnit = 20;

        public void Draw()
        {
            if (GUILayout.Button((IsExpanded ? "▼ " : "▶ ") + Name, ComponentMenu.LabelLeftStyle))
            {
                IsExpanded = !IsExpanded;
            }
            if (IsExpanded)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(IndentUnit);

                GUILayout.BeginVertical();
                DrawExpanded();
                GUILayout.EndVertical();

                GUILayout.EndHorizontal();
            }
        }

        protected abstract void DrawExpanded();
    }
}
