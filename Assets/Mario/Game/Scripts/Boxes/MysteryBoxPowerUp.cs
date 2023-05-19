using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class MysteryBoxPowerUp : BottomHitableBox
    {
        private GameObject _itemPrefab;

        [SerializeField] private AudioSource _risingSoundFX;
        [SerializeField] protected MysteryBoxPowerUpProfile _powerUpBoxProfile;
        [SerializeField] private Animator _spriteAnimator;

        public override void OnHitFromBottom(PlayerController player)
        {
            _hitSoundFX.Play();

            if (!IsHitable)
                return;

            base.OnHitFromBottom(player);
            _spriteAnimator.SetTrigger("Disable");
            _risingSoundFX.Play();

            _itemPrefab = player.Mode == Enums.PlayerModes.Small ? _powerUpBoxProfile.RedMushroom : _powerUpBoxProfile.Flower;
        }
        public override void OnJumpCompleted()
        {
            base.InstantiateContent(_itemPrefab);
        }
    }
}