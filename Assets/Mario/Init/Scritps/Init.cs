using UnityEngine;

namespace Mario.Init
{
    public class Init : MonoBehaviour
    {
        private void Awake()
        {
            Screen.SetResolution(1200, 1050, true);
            UnityEngine.Application.targetFrameRate = 60;
        }
    }
}