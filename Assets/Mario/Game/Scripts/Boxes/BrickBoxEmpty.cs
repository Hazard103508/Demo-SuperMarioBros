using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class BrickBoxEmpty : Box
    {
        #region Objects
        [SerializeField] private BrickBoxEmptyProfile _brickProfile;
        #endregion

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            AllServices.AddressablesService.AddAsset(_brickProfile.BrokenBrickReference);
        }
        #endregion

        #region Protected Methods
        protected override void OnJumpCompleted()
        {
            base.OnJumpCompleted();
            IsHitable = true;
        }
        #endregion

        #region Private Methods
        private IEnumerator InstantiateBreakedBox()
        {
            yield return new WaitForEndOfFrame();

            var prefab = AllServices.AddressablesService.GetAssetReference(_brickProfile.BrokenBrickReference);
            InstantiateContent(prefab);
            AllServices.ScoreService.Add(_brickProfile.Points);
            Destroy(gameObject);
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromBottom(PlayerController player)
        {
            PlayHitSoundFX();

            if (player.RawMovement.y > 0 || player.Input.JumpDown)
            {
                base.OnHittedByPlayerFromBottom(player);
                if (player.Mode != Enums.PlayerModes.Small)
                    StartCoroutine(InstantiateBreakedBox());
            }
            else
            {
                if (player.IsDucking)
                    return;
            }
        }
        #endregion
    }
}