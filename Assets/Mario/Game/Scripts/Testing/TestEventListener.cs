using Mario.Application.Services;
using Mario.Game.Enums;
using UnityEngine;

namespace Mario.Game.Player
{
    public class TestEventListener : MonoBehaviour
    {
        [SerializeField] private PlayerController player;

        public void OnEventListener_PowerUp() => player.Mode = player.Mode == PlayerModes.Small ? PlayerModes.Big : PlayerModes.Super;
        public void OnEventListener_Kill() => player.Kill();
        public void OnEventListener_AddLife() => AllServices.PlayerService.AddLife();
        public void OnEventListener_AddCoin() => AllServices.CoinService.Add();
    }
}