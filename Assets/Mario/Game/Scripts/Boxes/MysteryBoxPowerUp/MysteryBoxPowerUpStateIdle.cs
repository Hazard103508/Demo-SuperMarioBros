using Mario.Application.Services;
using Mario.Game.Boxes.Box;
using Mario.Game.Player;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Boxes.MysteryBoxPowerUp
{
    public class MysteryBoxPowerUpStateIdle : BoxStateIdle
    {
        #region Properties
        new protected MysteryBoxPowerUp Box => (MysteryBoxPowerUp)base.Box;
        #endregion

        #region Constructor
        public MysteryBoxPowerUpStateIdle(Box.Box box) : base(box)
        {
        }
        #endregion

        #region Private Methods
        private IEnumerator InstancePoweUp(PlayerController_OLD player)
        {
            yield return new WaitForSeconds(0.2f);
            if (player.Mode == Enums.PlayerModes.Small)
                Services.PoolService.GetObjectFromPool(Box.Profile.MushroomPoolReference, Box.transform.position);
            else
                Services.PoolService.GetObjectFromPool(Box.Profile.FlowerPoolReference, Box.transform.position);
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Box.IsLastJump = true;
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromBottom(PlayerController_OLD player)
        {
            Services.PoolService.GetObjectFromPool(Box.Profile.RiseItemSoundFXPoolReference, Box.transform.position);
            Box.StartCoroutine(InstancePoweUp(player));
            base.OnHittedByPlayerFromBottom(player);
        }
        #endregion
    }
}