using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class PowerUpBox : MysteryBox
    {
        [SerializeField] private GameObject _redMushroom;
        [SerializeField] private GameObject _flower;

        public override void OnHitFromBottom(PlayerController player)
        {
            base._contentPrefab = player.Mode == Enums.PlayerModes.Small ? _redMushroom : _flower;
            base.OnHitFromBottom(player);
        }
    }
}