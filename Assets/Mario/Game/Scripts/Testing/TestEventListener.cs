using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Player
{
    public class TestEventListener : MonoBehaviour
    {
        [SerializeField] private PlayerController player;

        public void OnEventListener_Buff() => player.Buff();
        public void OnEventListener_Nerf() => player.Nerf();
        public void OnEventListener_Kill() => player.Kill();
        public void OnEventListener_AddLife() => AllServices.PlayerService.AddLife();
        public void OnEventListener_AddCoin() => AllServices.CoinService.Add();
    }
}