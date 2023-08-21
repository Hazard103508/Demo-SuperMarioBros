using Mario.Application.Services;
using Mario.Game.Boxes.Box;
using Mario.Game.Player;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Boxes.BrickBoxEmpty
{
    public class BrickBoxEmptyStateIdle : BoxStateIdle
    {
        #region Properties
        new protected BrickBoxEmpty Box => (BrickBoxEmpty)base.Box;
        #endregion

        #region Constructor
        public BrickBoxEmptyStateIdle(Box.Box box) : base(box)
        {
        }
        #endregion

        #region Private Methods
        private IEnumerator DestroyBox()
        {
            yield return new WaitForEndOfFrame();
            GameObject.Destroy(Box.gameObject);
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromBottom(PlayerController_OLD player)
        {
            if (player.Mode != Enums.PlayerModes.Small)
            {
                Services.PoolService.GetObjectFromPool(Box.Profile.BrokenBrickPoolReference, Box.transform.position);
                Services.ScoreService.Add(Box.Profile.Points);
                Box.StartCoroutine(DestroyBox()); // need time to change state first
            }
            base.OnHittedByPlayerFromBottom(player);
        }
        #endregion
    }
}