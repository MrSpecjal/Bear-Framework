using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace BearFramework.Tools
{
    public class HideFlagsExplorer : EditorWindow
    {

        [MenuItem("Bear Framework/Tools/Hide Flags Explorer")]
        static void Init()
        {
            GetWindow<HideFlagsExplorer>();
        }

        readonly List<GameObject> objects = new List<GameObject>();
        Vector2 scrollPos = Vector2.zero;
        bool filterTop = true;
        bool filterHidden = false;

        void OnEnable()
        {
            FindObjects();
        }

        void AddObject(GameObject obj)
        {
            if (filterTop)
            {
                obj = obj.transform.root.gameObject;
            }
            if (filterHidden)
            {
                if ((obj.hideFlags & (HideFlags.HideInHierarchy | HideFlags.HideInInspector)) == 0) return;
            }
            if (!objects.Contains(obj))
            {
                objects.Add(obj);
            }
        }

        void FindObjects()
        {
            var objs = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
            objects.Clear();
            foreach (var obj in objs) AddObject(obj);
        }

        HideFlags HideFlagsButton(string aTitle, HideFlags aFlags, HideFlags aValue)
        {
            if (GUILayout.Toggle((aFlags & aValue) > 0, aTitle, "Button"))
            {
                aFlags |= aValue;
            }
            else
            {
                aFlags &= ~aValue;
            }
            return aFlags;
        }

        void OnGUI()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Find Game Objects")) FindObjects();
            filterTop = GUILayout.Toggle(filterTop, "Only Top Objects");
            filterHidden = GUILayout.Toggle(filterHidden, "Only Hidden Objects");
            GUILayout.EndHorizontal();
            scrollPos = GUILayout.BeginScrollView(scrollPos);
            for (int i = 0; i < objects.Count; i++)
            {
                GameObject obj = objects[i];
                if (obj == null) continue;
                GUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(obj.name, obj, typeof(GameObject), true);
                HideFlags flags = obj.hideFlags;
                flags = HideFlagsButton("Hide In Hierarchy", flags, HideFlags.HideInHierarchy);
                flags = HideFlagsButton("Hide In Inspector", flags, HideFlags.HideInInspector);
                flags = HideFlagsButton("Don't Save", flags, HideFlags.DontSave);
                flags = HideFlagsButton("Not Editable", flags, HideFlags.NotEditable);
                obj.hideFlags = flags;
                GUILayout.Label("" + ((int)flags), GUILayout.Width(20));
                GUILayout.Space(20);
                if (GUILayout.Button("Delete"))
                {
                    DestroyImmediate(obj);
                    FindObjects();
                    GUIUtility.ExitGUI();
                }
                GUILayout.Space(20);
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
        }
    }
}