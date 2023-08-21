using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using UnityEngine;
using UnityShared.Behaviours.Various.RaycastRange;

namespace Mario.Game.Boxes
{
    public class BrickBoxCoin : Box.Box
    {
        #region Objects
        //[SerializeField] private Animator _spriteAnimator;
        
        //private float _limitTime;
        //private bool _firstHit;
        //private bool _isEmpty;
        #endregion

        #region Properties
        new public BrickBoxCoinProfile Profile => (BrickBoxCoinProfile)base.Profile;
        #endregion

        //#region Unity Methods
        //protected override void Awake()
        //{
        //    base.Awake();
        //    _limitTime = Profile.LimitTime;
        //}
        //private void Update()
        //{
        //    if (_limitTime > 0 && _firstHit)
        //        _limitTime -= Time.deltaTime;
        //}
        //#endregion
        //
        //#region Protected Methods
        //protected override void OnJumpCompleted()
        //{
        //    base.OnJumpCompleted();
        //    IsHitable = !_isEmpty;
        //
        //    //_spriteAnimator.SetTrigger(_isEmpty ? "Disable" : "Idle");
        //}
        //#endregion
        //
        //#region On Player Hit
        //public override void OnHittedByPlayerFromBottom(PlayerController_OLD player)
        //{
        //    if (!IsHitable)
        //        return;
        //
        //    PlayHitSoundFX();
        //    _firstHit = true;
        //
        //    base.OnHittedByPlayerFromBottom(player);
        //
        //    if (_limitTime < 0)
        //    {
        //        _isEmpty = true;
        //        _spriteAnimator.SetTrigger("LastHit");
        //    }
        //    else
        //        _spriteAnimator.SetTrigger("Hit");
        //
        //    base.ShowContent(Profile.CoinPoolReference);
        //}
        //#endregion
    }
}