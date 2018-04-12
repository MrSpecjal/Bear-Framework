using UnityEngine;

namespace BearFramework.ExtendedUnityEditor
{
    [AddComponentMenu("Bear Framework/Gizmos/Cube Trigger Gizmo")]
    [RequireComponent(typeof(BoxCollider))]
    public class DrawCubeTrigger : MonoBehaviour
    {
        public Color standardColor;
        public Color onSelectedColor;

        BoxCollider m_BoxCollider;
        BoxCollider BoxCollider
        {
            get
            {
                if (m_BoxCollider == null)
                {
                    m_BoxCollider = GetComponent<BoxCollider>();
                }

                return m_BoxCollider;
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = new Color(standardColor.r, standardColor.g, standardColor.b, 1f);
            Gizmos.DrawWireCube(transform.position + BoxCollider.center, BoxCollider.size);

            Gizmos.color = new Color(standardColor.r, standardColor.g, standardColor.b, 0.3f);
            Gizmos.DrawCube(transform.position + BoxCollider.center, BoxCollider.size);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(onSelectedColor.r, onSelectedColor.g, onSelectedColor.b, 1f);
            Gizmos.DrawWireCube(transform.position + BoxCollider.center, BoxCollider.size);

            Gizmos.color = new Color(onSelectedColor.r, onSelectedColor.g, onSelectedColor.b, 0.3f);
            Gizmos.DrawCube(transform.position + BoxCollider.center, BoxCollider.size);
        }
    }
}