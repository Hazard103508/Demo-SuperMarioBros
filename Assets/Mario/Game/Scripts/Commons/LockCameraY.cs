using Cinemachine;
using UnityEngine;

namespace Mario.Game.Commons
{
    /// <summary>
    /// An add-on module for Cinemachine Virtual Camera that locks the camera's Y co-ordinate
    /// </summary>
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")] // Hide in menu
    public class LockCameraY : CinemachineExtension
    {
        [Tooltip("Lock the camera's Y position to this value")]
        [SerializeField] private float _yPosition;

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Body)
            {
                var pos = state.RawPosition;
                pos.y = _yPosition;
                state.RawPosition = pos;
            }
        }
    }
}