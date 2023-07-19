using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Npc
{
    public class Goomba : NPC
    {
        [SerializeField] private GoombaProfile _profile;

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            _currentSpeed = Vector2.right * _profile.MoveSpeed;
        }
        #endregion

        #region Protected Properties
        protected override float Profile_FallSpeed => _profile.FallSpeed;
        protected override float Profile_MaxFallSpeed => _profile.MaxFallSpeed;
        protected override int Profile_PointsHit => _profile.Points;
        protected override int Profile_PointsKill => _profile.Points;
        protected override float Profile_JumpAcceleration => _profile.JumpAcceleration;
        #endregion
    }
}