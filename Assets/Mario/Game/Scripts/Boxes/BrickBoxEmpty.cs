using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
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
            _hitSoundFX.Play();

            if (player.RawMovement.y > 0 || player.Input.JumpDown)
            {
                if (player.Mode == Enums.PlayerModes.Small)
                    base.OnHitFromBottom(player);
                else
                {
                    var prefab = AllServices.SceneService.GetAssetReference<GameObject>(_brickProfile.BrokenBrickReference);
                    InstantiateContent(prefab);
                    AllServices.ScoreService.Add(_brickProfile.Points);
                    AudioSource.PlayClipAtPoint(_hitSoundFX.clip, transform.position);
                    Destroy(gameObject);
                }
            }
            else
            {
                if (player.IsDucking)
                    return;

                player.IsStuck = true;
            }
        }

        public override void OnJumpCompleted()
        {
            base.OnJumpCompleted();
            IsHitable = true;
        }
    }
}