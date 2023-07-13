using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class BrickBoxEmpty : BottomHitableBox
    {
        [SerializeField] private BrickBoxEmptyProfile _brickProfile;

        protected override void Awake()
        {
            base.Awake();
            AllServices.SceneService.AddAsset(_brickProfile.BrokenBrickReference);
        }
        public override void OnHitFromBottom(PlayerController player)
        {
            PlayHitSoundFX();

            if (player.RawMovement.y > 0 || player.Input.JumpDown)
            {
                base.OnHitFromBottom(player);
                if (player.Mode != Enums.PlayerModes.Small)
                    StartCoroutine(InstantiateBreakedBox());
            }
            else
            {
                if (player.IsDucking)
                    return;
            }
        }
        private IEnumerator InstantiateBreakedBox()
        {
            yield return new WaitForEndOfFrame();

            var prefab = AllServices.SceneService.GetAssetReference<GameObject>(_brickProfile.BrokenBrickReference);
            InstantiateContent(prefab);
            AllServices.ScoreService.Add(_brickProfile.Points);
            Destroy(gameObject);
        }

        public override void OnJumpCompleted()
        {
            base.OnJumpCompleted();
            IsHitable = true;
        }
    }
}