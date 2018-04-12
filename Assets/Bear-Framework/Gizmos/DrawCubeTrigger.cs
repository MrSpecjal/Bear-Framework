using UnityEngine;

namespace BearFramework.ExtendedUnityEditor
{
    [AddComponentMenu("Bear Framework/Gizmos/Cube Trigger Gizmo")]
    [RequireComponent(typeof(BoxCollider))]
    public class DrawCubeTrigger : MonoBehaviour
    {
        public Color standardColor;
        public Color onSelectedColor;

        BoxCollider boxCollider;
        BoxCollider BoxCollider
        {
            get
            {
                if (boxCollider == null)
                {
                    boxCollider = GetComponent<BoxCollider>();
                }

                return boxCollider;
            }
        }

        void OnDrawGizmos()
        {
            DrawGizmo(standardColor);
        }

        void OnDrawGizmosSelected()
        {
            DrawGizmo(onSelectedColor);
        }

        void DrawGizmo(Color color)
        {
            color.a = 1f;
            Gizmos.color = color;
            Gizmos.DrawWireCube(transform.position + BoxCollider.center, BoxCollider.size);

            color.a = 0.3f;
            Gizmos.color = color;
            Gizmos.DrawCube(transform.position + BoxCollider.center, BoxCollider.size);
        }
    }
}