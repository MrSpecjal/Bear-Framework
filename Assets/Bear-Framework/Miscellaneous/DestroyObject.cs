using UnityEngine;

namespace BearFramework.Miscellaneous
{
    [AddComponentMenu("Bear Framework/Miscellaneous/Destroy Object")]
    public class DestroyObject : MonoBehaviour
    {
        public void DestroySelfGameObject()
        {
            GameObject.Destroy(gameObject);
        }

        public void DestroyOtherGameObject(GameObject otherTarget)
        {
            GameObject.Destroy(otherTarget);
        }
    }
}
