using UnityEngine;

namespace UnityShared.Behaviours.Controllers.Players.TarodevController
{

    public struct FrameInput
    {
        public float X, Y;
        public bool JumpDown;
        public bool JumpUp;
    }

    public interface IPlayerController
    {
        public Vector3 Velocity { get; }
        public FrameInput Input { get; }
        public bool JumpingThisFrame { get; }
        public bool LandingThisFrame { get; }
        public Vector3 RawMovement { get; }
        public bool Grounded { get; }
    }

    public interface IExtendedPlayerController : IPlayerController
    {
        public bool DoubleJumpingThisFrame { get; set; }
        public bool Dashing { get; set; }
    }


}