using UnityEngine;
using UnityEditor;

public class CameraViewer : EditorWindow
{
    bool updateCameraView = false;
    Camera camera;
    RenderTexture renderTexture, originalTarget;

    [MenuItem("Bear Framework/Tools/Camera View")]
    static void Launch()
    {
        EditorWindow editorWindow = GetWindow(typeof(CameraViewer));

        editorWindow.Show();
    }

    void Update()
    {
        if (camera != null)
        {
            camera.Render();
            if (updateCameraView)
            {
                Repaint();
            }
        }
    }

    void OnSelectionChange()
    {
        Camera newCamera = (Selection.activeTransform == null) ? null : Selection.activeTransform.gameObject.GetComponent<Camera>();

        if (newCamera != camera)
        {
            if (originalTarget != null)
            {
                camera.targetTexture = originalTarget;
            }

            camera = newCamera;
            if (camera != null)
            {
                originalTarget = camera.targetTexture;
                camera.targetTexture = renderTexture;
            }
            else
            {
                originalTarget = null;
            }
        }
    }

    void OnGUI()
    {
        if (camera == null)
        {
            ToolbarGUI("No camera selection");
            return;
        }

        if (renderTexture == null || renderTexture.width != position.width || renderTexture.height != position.height)
        {
            renderTexture = new RenderTexture((int)position.width, (int)position.height, (int)RenderTextureFormat.ARGB32);
            camera.targetTexture = renderTexture;
        }

        GUI.DrawTexture(new Rect(0.0f, 0.0f, position.width, position.height), renderTexture);

        ToolbarGUI(camera.gameObject.name);
    }

    void ToolbarGUI(string title)
    {
        GUILayout.BeginHorizontal("Toolbar");
        GUILayout.Label(title);
        GUILayout.FlexibleSpace();
        updateCameraView = GUILayout.Toggle(updateCameraView, "Update View", "ToolbarButton");
        GUILayout.EndHorizontal();
    }
}
