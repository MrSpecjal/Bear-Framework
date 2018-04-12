using UnityEditor;
using UnityEngine;

namespace BearFramework.ExtendedUnityEditor
{
    [CustomEditor(typeof(PathEditor))]
    public class PathEditorGizmos : Editor
    {
        PathEditor pathEditor;

        void OnEnable()
        {
            pathEditor = (PathEditor)target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }

        void OnSceneGUI()
        {
            Handles.BeginGUI();
            GUI.color = pathEditor.pathColor;
            Handles.Label(pathEditor.transform.position + new Vector3(0, 1, 0), "Path name: " + pathEditor.name);
            Handles.EndGUI();
        }
    }
}