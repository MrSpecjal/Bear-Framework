using System.Collections.Generic;
using UnityEngine;

namespace BearFramework.ExtendedUnityEditor
{
    [ExecuteInEditMode]
    public class PathEditor : MonoBehaviour
    {
        public Color pathColor = Color.white;
        public List<Transform> waypoints = new List<Transform>();
        Transform[] path;

        private void OnDrawGizmos()
        {
            Gizmos.color = pathColor;
            path = GetComponentsInChildren<Transform>();
            waypoints.Clear();

            foreach (Transform waypoint in path)
            {
                if (waypoint != this.transform)
                {
                    waypoints.Add(waypoint);
                }
            }

            for (int i = 0; i <= waypoints.Count; i++)
            {
                Vector3 currentPosition = path[i].position;
                if (i > 0)
                {
                    Vector3 previousPosition = path[i - 1].position;
                    Gizmos.DrawLine(previousPosition, currentPosition);
                    Gizmos.DrawWireSphere(currentPosition, 0.3f);
                    Gizmos.DrawSphere(currentPosition, 0.25f);
                }
            }
        }
    }
}