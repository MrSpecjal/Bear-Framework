using UnityEngine;
using UnityEditor;
using System.IO;

namespace Bearpathramework.Tools
{
    [ExecuteInEditMode]
    public class SetUpProject : MonoBehaviour
    {
        [MenuItem("Bear Framework/Setup .../Project Folders")]
        static void MenuMakepatholders()
        {
            Createpatholders();
        }

        static void Createpatholders()
        {
            string path = Application.dataPath + "/";

            if (Directory.Exists(path + "_Animations"))
                return;

            Directory.CreateDirectory(path + "_Animations");
            Directory.CreateDirectory(path + "_Animations/Animations");
            Directory.CreateDirectory(path + "_Animations/Animators");
            Directory.CreateDirectory(path + "_Assets");
            Directory.CreateDirectory(path + "_Audio");
            Directory.CreateDirectory(path + "_Audio/Music");
            Directory.CreateDirectory(path + "_Audio/SFX");
            Directory.CreateDirectory(path + "_Graphic");
            Directory.CreateDirectory(path + "_Graphic/UI");
            Directory.CreateDirectory(path + "_Graphic/Textures");
            Directory.CreateDirectory(path + "_Graphic/Fonts");
            Directory.CreateDirectory(path + "_Graphic/Materials");
            Directory.CreateDirectory(path + "_Graphic/Models");
            Directory.CreateDirectory(path + "_Scenes");
            Directory.CreateDirectory(path + "_Scenes/Prototypes");
            Directory.CreateDirectory(path + "_Scenes/Story");
            Directory.CreateDirectory(path + "_Scripts");
            Directory.CreateDirectory(path + "_Shaders");
            Directory.CreateDirectory(path + "_Shaders/Shaders");
            Directory.CreateDirectory(path + "_Shaders/PP");
            Directory.CreateDirectory(path + "_Prefabs");
            Directory.CreateDirectory(path + "_Videos");
            Directory.CreateDirectory(path + "Resources");

            EditorApplication.update();
        }
    }
}