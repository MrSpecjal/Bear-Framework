using UnityEditor;
using UnityEngine;

namespace BearFramework.ExtendedUnityEditor
{

    [ExecuteInEditMode]
    public class ExtendedPrimitives : MonoBehaviour
    {
        static Transform target;

        [MenuItem("GameObject/3D Object/Ball")]
        private static void SpawnBall()
        {
            GameObject ball = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/WindEngine/Extended Primitives/Ball.fbx", typeof(GameObject));
            Instantiate(ball, target);
        }

        [MenuItem("GameObject/3D Object/Cone")]
        private static void SpawnCone()
        {
            GameObject cone = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/WindEngine/Extended Primitives/Cone.fbx", typeof(GameObject));
            Instantiate(cone, target);
        }

        [MenuItem("GameObject/3D Object/Icosphere")]
        private static void SpawnIcosphere()
        {
            GameObject icosphere = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/WindEngine/Extended Primitives/Icosphere.fbx", typeof(GameObject));
            Instantiate(icosphere, target);

        }

        [MenuItem("GameObject/3D Object/Torus")]
        private static void SpawnTorus()
        {
            SceneView sceneView = SceneView.lastActiveSceneView;
            Debug.Log(sceneView.pivot + " '''' " + sceneView.rotation);
            target.position = sceneView.pivot;
            GameObject torus = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/WindEngine/Extended Primitives/Torus.fbx", typeof(GameObject));
            Instantiate(torus, target);
        }
    }
}