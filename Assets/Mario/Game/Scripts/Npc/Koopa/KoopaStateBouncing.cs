using Mario.Game.Interfaces;
using Mario.Game.Player;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc.Koopa
{
    public class KoopaStateBouncing : IKoopaState
    {
        private Koopa _koopa;

        public KoopaStateBouncing(Koopa koopa)
        {
            _koopa = koopa;
        }

        #region IState Methods
        public void Enter()
        {

        }
        public void Exit()
        {

        }
        public void Update()
        {

        }
        #endregion

        #region IKoopaState Methods
        public void OnLeftCollided(RayHitInfo hitInfo)
        {

        }
        public void OnRightCollided(RayHitInfo hitInfo)
        {

        }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromTop(PlayerController player) { }
        public void OnHittedByPlayerFromLeft(PlayerController player) { }
        public void OnHittedByPlayerFromRight(PlayerController player) { }
        public void OnHittedByPlayerFromBottom(PlayerController player) => player.DamagePlayer();
        #endregion

        #region On Box Hit
        public void OnHittedByBox(GameObject box) => _koopa.Kill(box.transform.position);
        #endregion
    }
}