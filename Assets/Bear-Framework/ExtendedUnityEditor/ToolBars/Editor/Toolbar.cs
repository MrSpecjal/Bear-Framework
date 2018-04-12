using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace BearFramework.Toolbars
{
    public class Toolbar : EditorWindow, IHasCustomMenu
    {
        private const string TITLE = "Toolbar";
        private const float WINDOW_WIDTH = 32;
        private const float WINDOW_HEIGHT = 28;
        public Vector2 scrollPossition;

        private sealed class DataImageBased
        {
            public readonly string command;

            public DataImageBased(string command)
            {
                this.command = command;
            }
        }

        private class DataEditorBased
        {
            public readonly Type type;
            public readonly string commandRoot;
            public readonly string commandName;

            public DataEditorBased() { }

            public DataEditorBased(Type type, string commandRoot, string commandName)
            {
                this.type = type;
                this.commandRoot = commandRoot;
                this.commandName = commandName;
            }
        }

        private static readonly GUILayoutOption[] BUTTON_OPTIONS =
        {
            GUILayout.MinWidth(28),
            GUILayout.MaxWidth(48),
            GUILayout.Height(24),
        };

        private static readonly DataEditorBased[] DataEditorBased_LIST =
        {
            new DataEditorBased(typeof(GameObject), "GameObject/", "Create Empty Child"),
            new DataEditorBased(typeof(Text), "GameObject/UI/", "Text"),
            new DataEditorBased(typeof(Image), "GameObject/UI/", "Image"),
            new DataEditorBased(typeof(RawImage), "GameObject/UI/", "Raw Image"),
            new DataEditorBased(typeof(Button), "GameObject/UI/", "Button"),
            new DataEditorBased(typeof(Toggle), "GameObject/UI/", "Toggle"),
            new DataEditorBased(typeof(Slider), "GameObject/UI/", "Slider"),
            new DataEditorBased(typeof(Scrollbar), "GameObject/UI/", "Scrollbar"),
            new DataEditorBased(typeof(Dropdown), "GameObject/UI/", "Dropdown"),
            new DataEditorBased(typeof(InputField), "GameObject/UI/", "Input Field"),
            new DataEditorBased(typeof(Canvas), "GameObject/UI/", "Canvas"),
            new DataEditorBased(typeof(ScrollRect), "GameObject/UI/", "Scroll View"),
            new DataEditorBased(),
            new DataEditorBased(typeof(Shadow), "Component/UI/Effects/", "Shadow"),
            new DataEditorBased(typeof(Outline), "Component/UI/Effects/", "Outline"),
            new DataEditorBased(typeof(PositionAsUV1), "Component/UI/Effects/", "Position As UV1"),
            new DataEditorBased(typeof(Mask), "Component/UI/", "Mask"),
            new DataEditorBased(typeof(RectMask2D), "Component/UI/", "Rect Mask 2D")
        };

        private static readonly DataImageBased[] BUTTON_DATA_LIST =
        {
            new DataImageBased("Input"),
            new DataImageBased("Tags and Layers"),
            new DataImageBased("Audio"),
            new DataImageBased("Time"),
            new DataImageBased("Player"),
            new DataImageBased("Physics"),
            new DataImageBased("Physics 2D"),
            new DataImageBased("Quality"),
            new DataImageBased("Graphics"),
            new DataImageBased("Network"),
            new DataImageBased("Editor"),
            new DataImageBased("Script Execution Order")
        };

        private string dir;

        private ToolbarSettings settings;
        private ToolbarSettings Settings
        {
            get
            {
                return settings ?? (settings = AssetDatabase.LoadAssetAtPath<ToolbarSettings>(dir + "/ToolbarSettings.asset"));
            }
        }
        private bool IsVertical
        {
            get
            {
                return Settings.isVertical;
            }
            set
            {
                Settings.isVertical = value;
            }
        }

        [MenuItem("Bear Framework/Tools/Toolbar")]
        private static void Init()
        {
            var win = GetWindow<Toolbar>(TITLE);

            var pos = win.position;
            pos.width = 640;
            pos.height = WINDOW_HEIGHT;
            win.position = pos;

            win.minSize = new Vector2(WINDOW_WIDTH, WINDOW_HEIGHT);

            var maxSize = win.maxSize;
            maxSize.y = WINDOW_HEIGHT;
            win.maxSize = maxSize;
        }

        private void OnGUI()
        {
            if (IsVertical)
            {
                scrollPossition = EditorGUILayout.BeginScrollView(scrollPossition, GUILayout.Width(102), GUILayout.Height(Screen.height));
                GUILayout.BeginVertical();
            }
            else
            {
                scrollPossition = EditorGUILayout.BeginScrollView(scrollPossition, GUILayout.Width(Screen.width), GUILayout.Height(28));
                GUILayout.BeginHorizontal();
            }
            GUILayout.Label("UI Toolbar:");
            foreach (var n in DataEditorBased_LIST)
            {
                var type = n.type;
                if (type == null)
                {
                    var options = IsVertical
                        ? new[] { GUILayout.MinWidth(28), GUILayout.MaxWidth(48), GUILayout.Height(1) }
                        : new[] { GUILayout.Height(24), GUILayout.Width(1) }
                    ;
                    GUILayout.Box(string.Empty, options);
                    continue;
                }
                var commandName = n.commandName;
                var src = EditorGUIUtility.ObjectContent(null, type);
                var content = new GUIContent(src);
                content.text = string.Empty;
                content.tooltip = commandName;
                if (content == null) continue;
                var image = content.image;
                if (!GUILayout.Button(content, BUTTON_OPTIONS)) continue;
                var commandRoot = n.commandRoot;
                var menuItemPath = string.Format("{0}{1}", commandRoot, commandName);
                EditorApplication.ExecuteMenuItem(menuItemPath);
            }
            GUILayout.Label("Project Settings Toolbar:");
            foreach (var n in BUTTON_DATA_LIST)
            {
                var command = n.command;
                var path = string.Format("{0}/Textures/{1}.png", dir, command);
                var image = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
                var content = new GUIContent(image, command);
                if (GUILayout.Button(content, BUTTON_OPTIONS))
                {
                    var menuItemPath = string.Format("Edit/Project Settings/{0}", command);
                    EditorApplication.ExecuteMenuItem(menuItemPath);
                }
            }
            GUILayout.EndScrollView();
            if (IsVertical)
            {
                GUILayout.EndVertical();
            }
            else
            {
                GUILayout.EndHorizontal();
            }
        }

        public void AddItemsToMenu(GenericMenu menu)
        {
            menu.AddItem(new GUIContent("Vertical"), IsVertical, () =>
            {
                IsVertical = !IsVertical;
            });
        }

        private void OnEnable()
        {
            var mono = MonoScript.FromScriptableObject(this);
            var path = AssetDatabase.GetAssetPath(mono);
            dir = Path.GetDirectoryName(path);
        }
    }
}