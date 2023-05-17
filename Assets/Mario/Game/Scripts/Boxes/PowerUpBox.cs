using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class PowerUpBox : BottomHitableBlock
    {
        private GameObject _itemPrefab;

        [SerializeField] protected MysteryBoxPowerUpProfile _powerUpBoxProfile;
        [SerializeField] private Animator _spriteAnimator;

        public override void OnHitFromBottom(PlayerController player)
        {
            if (!IsHitable)
                return;

            base.OnHitFromBottom(player);
            _spriteAnimator.SetTrigger("Disable");

            _itemPrefab = player.Mode == Enums.PlayerModes.Small ? _powerUpBoxProfile.RedMushroom : _powerUpBoxProfile.Flower;
        }
        public override void OnJumpCompleted()
        {
            base.InstantiateContent(_itemPrefab);
        }
    }
}