using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BearFramework.ExtendedUnityEditor
{
  [InitializeOnLoad]
  public class ExtendedHierarchy
  {
    static Dictionary<string, Texture> icons = new Dictionary<string, Texture>()
    {
      { "Light", EditorGUIUtility.ObjectContent(null, typeof(Light)).image },
      { "MonoBehaviour", EditorGUIUtility.IconContent("cs Script Icon").image },
      { "ParticleSystem", EditorGUIUtility.ObjectContent(null, typeof(ParticleSystem)).image },
      { "Camera", EditorGUIUtility.IconContent("Camera Icon").image },
      { "MeshRenderer", EditorGUIUtility.ObjectContent(null, typeof(MeshRenderer)).image },
      { "Rigidbody", EditorGUIUtility.ObjectContent(null, typeof(Rigidbody)).image },
      { "Canvas", EditorGUIUtility.ObjectContent(null, typeof(Canvas)).image }
    };

    static int iconWidth = 18;
    static int iconHeight = 18;
    static int marginBetweenIcons = 18;

    static ExtendedHierarchy()
    {
      EditorApplication.hierarchyWindowItemOnGUI = DrawExtendedHierarchy;
    }

    static void DrawExtendedHierarchy(int instanceID, Rect selectedRect)
    {
      GameObject gameobject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

      if (gameobject != null)
      {
        Rect iconsPosition = new Rect(selectedRect);
        iconsPosition.x = 0;
        iconsPosition.width = iconWidth;
        iconsPosition.height = iconHeight;
        gameobject.SetActive(GUI.Toggle(new Rect(iconsPosition), gameobject.activeSelf, ""));
        int iconCount = 0;
        foreach (KeyValuePair<string, Texture> icon in icons)
        {
          iconCount += 1;
          if (gameobject.GetComponent(icon.Key) != null)
          {
            DisplayIcon(icon.Value, marginBetweenIcons * iconCount, selectedRect);
          }
        }
      }
    }

    static void DisplayIcon(Texture icon, int position, Rect selectedRect)
    {
      Rect iconsPosition = new Rect();
      iconsPosition.x = selectedRect.width - position;
      iconsPosition.y = selectedRect.y;
      iconsPosition.width = iconWidth;
      iconsPosition.height = iconHeight;
      GUI.Label(iconsPosition, icon);
    }
  }
}