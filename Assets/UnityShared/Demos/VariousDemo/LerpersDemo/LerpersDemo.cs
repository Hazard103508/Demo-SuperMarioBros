using UnityEngine;
using UnityShared.Behaviours.Various.Lerpers;

namespace UnityShared.Demos
{
    public class LerpersDemo : MonoBehaviour
    {
        [SerializeField] private LocalPositionLerper _positionLerper;
        [SerializeField] private LocalRotationLerper _rotationLerper;
        [SerializeField] private LocalScaleLerper _localScaleLerper;
        private bool isRunningForward;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isRunningForward)
                {
                    isRunningForward = false;
                    _positionLerper.RunBackwards();
                    _rotationLerper.RunBackwards();
                    _localScaleLerper.RunBackwards();
                }
                else
                {
                    isRunningForward = true;
                    _positionLerper.RunForward();
                    _rotationLerper.RunForward();
                    _localScaleLerper.RunForward();
                }
            }
        }
    }
}