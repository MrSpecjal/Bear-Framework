using UnityEngine;
using UnityEditor;

namespace BearFramework.Events
{
    [CustomEditor(typeof(CustomTimer))]
    public class CustomTimerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            CustomTimer customTimer = (CustomTimer)target;

            EditorGUILayout.LabelField("Timer: " + customTimer.timerCounter.ToString("F2"));

            EditorGUILayout.BeginHorizontal();
            string pauseButtonName;
            pauseButtonName = (customTimer.isTimerWorking) ? "Pause Timer" : (customTimer.timerCounter > 0) ? "Resume Timer" : "Pause Timer" ;

            if (GUILayout.Button("Begin Timer"))
            {
                customTimer.BeginTimer();
            }
            
            if (GUILayout.Button(pauseButtonName))
            {
                customTimer.PauseTimer();
            }

            if (GUILayout.Button("Stop Timer"))
            {
                customTimer.StopTimer();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}