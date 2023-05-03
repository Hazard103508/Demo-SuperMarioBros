using UnityEngine;
using UnityShared.Behaviours.Various.Lerpers;

namespace UnityShared.Demos
{
    public class SlerpersDemo : MonoBehaviour
    {
        [SerializeField] private LocalPositionSlerper _positionSlerper;
        //[SerializeField] private RotationLerper _rotationLerper;
        //[SerializeField] private LocalScaleLerper _localScaleLerper;
        private bool isRunningForward;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isRunningForward)
                {
                    isRunningForward = false;
                    _positionSlerper.RunBackwards();
                    //_rotationLerper.RunBackwards();
                    //_localScaleLerper.RunBackwards();
                }
                else
                {
                    isRunningForward = true;
                    _positionSlerper.RunForward();
                    //_rotationLerper.RunForward();
                    //_localScaleLerper.RunForward();
                }
            }
        }
    }
}