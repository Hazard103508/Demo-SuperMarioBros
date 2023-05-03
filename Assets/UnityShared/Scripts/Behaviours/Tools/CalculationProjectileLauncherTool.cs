using UnityEditor;
using UnityEngine;
using UnityShared.Helpers;

namespace UnityShared.Behaviours.Tools
{
    public class CalculationProjectileLauncherTool : MonoBehaviour
    {
        public Transform startObject;
        public Transform endObject;

        [Range(0, 10)]
        public float h = 25f;
        [Range(1, 100)]
        public float g = 18f;

        public float H
        {
            get => h <= endObject.position.y - startObject.position.y ? h + endObject.position.y - startObject.position.y : h;
            set => h = value;
        }
        public float G
        {
            get => -g;
            set => g = value;
        }

        public LaunchData CalculateLauncherData()
        {
            Vector3 startPoint = startObject.position;
            Vector3 endPoint = endObject.position;

            float displacamentY = endPoint.y - startPoint.y;
            Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0, endPoint.z - startPoint.z);

            float time = MathEquations.Trajectory.GetTime(H, G, displacamentY);

            Vector3 velocityY = Vector3.up * MathEquations.Trajectory.GetVelocity(H, G);
            Vector3 velocityXZ = displacementXZ / time;

            return new LaunchData(velocityY + velocityXZ, time);
        }
#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Vector3 startPoint = startObject.position;
            Vector3 endPoint = endObject.position;

            LaunchData launchData = CalculateLauncherData();
            Vector3 previousDrawPoint = startPoint;

            int resolution = 30;
            for (int i = 0; i <= resolution; i++)
            {
                float simulationTime = i / (float)resolution * launchData.timeToTarget;
                Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * G * simulationTime * simulationTime / 2f;
                Vector3 drawPoint = startPoint + displacement;
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(previousDrawPoint, drawPoint);
                previousDrawPoint = drawPoint;
            }

            GUI.color = Color.cyan;
            Handles.Label(startPoint, "START");
            Handles.Label(endPoint, "END");
            GUI.color = Color.yellow;
            Handles.Label(new Vector3(startPoint.x, startPoint.y + 1, startPoint.z), $"Velocity: {launchData.initialVelocity.magnitude:f2}");
            Handles.Label(new Vector3(startPoint.x, startPoint.y + 1.5f, startPoint.z), $"Time: {launchData.timeToTarget:f2}");

        }
#endif

        public struct LaunchData
        {
            public readonly Vector3 initialVelocity;
            public readonly float timeToTarget;

            public LaunchData(Vector3 initialVelocity, float timeToTarget)
            {
                this.initialVelocity = initialVelocity;
                this.timeToTarget = timeToTarget;
            }

        }
    }
}