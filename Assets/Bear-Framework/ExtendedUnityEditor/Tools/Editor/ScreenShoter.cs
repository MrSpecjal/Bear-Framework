using System;
using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace BearFramework.Tools
{
    public class ScreenShoter : EditorWindow
    {
        int width = 1920;
        int height = 1080;
        string savePath = null;
        string pictureName = null;
        Camera screenShotCamera = null;

        [MenuItem("Bear Framework/Tools/Screen Shoter")]
        static void Init()
        {
            EditorWindow.GetWindow(typeof(ScreenShoter)).Show();
        }

        void OnInspectorUpdate()
        {
            Repaint();
        }

        private void OnGUI()
        {
            Texture2D logo = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/WindEngine/Graphic/bFramework-logo-title.png", typeof(Texture2D));
            GUILayout.Label(logo);

            screenShotCamera = (Camera)EditorGUI.ObjectField(new Rect(3, 490, position.width - 6, 20), "Screenshot Camera", screenShotCamera, typeof(Camera));

            EditorGUILayout.LabelField("Resolution");
            width = EditorGUILayout.IntField("Width: ", width);
            height = EditorGUILayout.IntField("Height: ", height);
            EditorGUILayout.BeginVertical();
            savePath = EditorGUILayout.TextField("File Path: ", savePath);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Browse"))
            {
                savePath = EditorUtility.OpenFolderPanel("Set Screenshot Save Path", "", "");
            }
            if (GUILayout.Button("Open this folder"))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = savePath,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            pictureName = EditorGUILayout.TextField("File Name: ", pictureName);

            GUILayout.Space(20);
            if (GUILayout.Button("Take Screanshot"))
            {
                TakeScreenShot();
            }
        }

        [ExecuteInEditMode]
        string GetScreenShotName()
        {
            var isoDateTimeFormat = CultureInfo.InvariantCulture.DateTimeFormat;
            string dateTime = DateTime.Now.ToString(isoDateTimeFormat.SortableDateTimePattern).Replace(":", "-");

            if (pictureName == "" || savePath == null)
            {
                return string.Format("{0}/{1}.png", savePath, dateTime);
            }
            else if(savePath == "" || savePath == null)
            {
                return string.Format("{0}/{1}.png", Application.dataPath.ToString(), pictureName);
            }
            else if( (pictureName == "" || savePath == null) && (savePath == "" || savePath == null) )
            {
                return string.Format("{0}/{1}.png", Application.dataPath.ToString(), dateTime);
            }
            else
            {
                return string.Format("{0}/{1}.png", savePath, pictureName);
            }
        }

        [ExecuteInEditMode]
        public void TakeScreenShot()
        {
            if (screenShotCamera == null)
            {
                screenShotCamera = Camera.main;
            }
            RenderTexture renderTexture = new RenderTexture(width, height, 24);
            screenShotCamera.targetTexture = renderTexture;

            Texture2D screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);
            screenShotCamera.Render();

            RenderTexture.active = renderTexture;
            screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);

            screenShotCamera.targetTexture = null;
            RenderTexture.active = null;

            Destroy(renderTexture);

            byte[] bytes = screenShot.EncodeToPNG();
            string fileName = GetScreenShotName();
            System.IO.File.WriteAllBytes(fileName, bytes);
        }
    }
}