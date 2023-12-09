using Cinemachine;
using Mario.Commons.Structs;
using UnityEngine;

namespace Mario.Game.Commons
{
    /// <summary>
    /// An add-on module for Cinemachine Virtual Camera that locks the camera's X co-ordinate
    /// </summary>
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")] // Hide in menu
    public class LockCameraX : CinemachineExtension
    {
        [Tooltip("Lock the camera's X position to this value")]
        public RangeNumber<float> XPosition;

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Finalize)
            {
                var pos = state.RawPosition;
                pos.x = Mathf.Clamp(pos.x, XPosition.Min, XPosition.Max);
                state.RawPosition = pos;

                if (pos.x > XPosition.Min)
                    XPosition.Min = pos.x;
            }
        }
    }
}