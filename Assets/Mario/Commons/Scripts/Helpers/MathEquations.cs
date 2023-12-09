using UnityEngine;

namespace Mario.Commons.Helpers
{
    public static class MathEquations
    {
        public static class Trajectory
        {
            /// <summary>
            /// Obtiene la velocidad que tendra el objeto en la trayectoria
            /// </summary>
            /// <param name="H">Altura</param>
            /// <param name="G">Gravedad</param>
            /// <returns></returns>
            public static float GetVelocity(float H, float G) => Mathf.Sqrt(H * -2f * G);
            /// <summary>
            /// Obtiene la altura que tendra el objeto en la trayectoria
            /// </summary>
            /// <param name="F">fuerza de salto</param>
            /// <param name="G">Gravedad</param>
            /// <returns></returns>
            public static float GetHeight(float F, float G) => Mathf.Pow(F, 2) / (-2f * G);
            /// <summary>
            /// 
            /// </summary>
            /// <param name="H">Altura</param>
            /// <param name="G">Gravedad</param>
            /// <param name="displacamentY">Diferencia en altura entre la posicion destino y la posicion origen</param>
            /// <returns></returns>
            public static float GetTime(float H, float G, float displacamentY) => Mathf.Sqrt(-2 * H / G) + Mathf.Sqrt(2 * (displacamentY - H) / G);
        }
    }
}