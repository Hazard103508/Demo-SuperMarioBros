using Mario.Game.Interfaces;
using Mario.Game.Player;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class PipeRight : Pipe, IHittableByPlayerFromLeft
    {
        #region Protected Method
        protected override IEnumerator OnMovePlayer(PlayerController player)
        {
            _playerService.EnableAutoWalk(true);
            while (player.transform.position.x < transform.position.x)
            {
                player.transform.Translate(2f * Time.deltaTime * Vector3.right);
                yield return null;
            }
        }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromLeft(PlayerController player)
        {
            if (player.InputActions.Move > 0)
                MoveIntoPipe(player);
        }
        #endregion
    }
}