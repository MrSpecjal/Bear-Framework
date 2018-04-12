using UnityEngine;
using UnityEditor;
using System.IO;

namespace BearFramework.Localization
{
    public class LocalizedTextEditor : EditorWindow
    {
        public Vector2 scrollPos;
        public LocalizationData localizationData;

        [MenuItem("Bear Framework/Localization/Localization Editor")]
        static void Init()
        {
            EditorWindow.GetWindow(typeof(LocalizedTextEditor)).Show();
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(0, 0, 500, Screen.height));
            GUILayout.BeginVertical();
            scrollPos = GUILayout.BeginScrollView(scrollPos, false, false, GUILayout.Width(500), GUILayout.MinHeight(200), GUILayout.MaxHeight(Screen.height - Screen.height / 50), GUILayout.ExpandHeight(true));
            if (localizationData != null)
            {
                SerializedObject serializedObject = new SerializedObject(this);
                SerializedProperty serializedProperty = serializedObject.FindProperty("localizationData");
                EditorGUILayout.PropertyField(serializedProperty, true);
                serializedObject.ApplyModifiedProperties();


                if (GUILayout.Button("Save data"))
                {
                    SaveGameData();
                }
            }

            if (GUILayout.Button("Load data"))
            {
                LoadGameData();
            }

            if (GUILayout.Button("Create new data"))
            {
                CreateNewData();
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        private void LoadGameData()
        {
            string filePath = EditorUtility.OpenFilePanel("Select localization data file", Application.streamingAssetsPath, "json");

            if (!string.IsNullOrEmpty(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);

                localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
            }
        }

        private void SaveGameData()
        {
            string filePath = EditorUtility.SaveFilePanel("Save localization data file", Application.streamingAssetsPath, "", "json");

            if (!string.IsNullOrEmpty(filePath))
            {
                string dataAsJson = JsonUtility.ToJson(localizationData);
                File.WriteAllText(filePath, dataAsJson);
            }
        }

        private void CreateNewData()
        {
            localizationData = new LocalizationData();
        }

    }
}