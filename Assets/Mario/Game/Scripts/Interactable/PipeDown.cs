using Mario.Game.Interfaces;
using Mario.Game.Player;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class PipeDown : Pipe, IHittableByPlayerFromTop
    {
        #region Protected Method
        protected override IEnumerator OnMovePlayer(PlayerController player)
        {
            _playerService.EnableInputs(false);
            while (player.transform.position.y > transform.position.y)
            {
                player.transform.Translate(4f * Time.deltaTime * Vector3.down);
                yield return null;
            }
        }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromTop(PlayerController player)
        {
            if (player.InputActions.Ducking)
                MoveIntoPipe(player);
        }
        #endregion
    }
}