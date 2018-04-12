using UnityEngine;

namespace BearFramework.Miscellaneous
{
    [AddComponentMenu("Bear Framework/Miscellaneous/Hide Object")]
    public class HideObject : MonoBehaviour
    {
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
