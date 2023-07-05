using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Player
{
    public class TestEventListener : MonoBehaviour
    {
        [SerializeField] private PlayerController player;

        private void Update()
        {
            // TESTING----------------

            if (Input.GetKeyDown(KeyCode.R))
                AllServices.SceneService.LoadMapScene(0);

            if (Input.GetKeyDown(KeyCode.KeypadPlus))
                AllServices.PlayerService.AddLife();

            if (Input.GetKeyDown(KeyCode.G))
                AllServices.CoinService.Add();


            if (Input.GetKeyDown(KeyCode.Space))
                Debug.Break();

            // TESTING----------------
        }

        public void OnEventListener_Buff() => player.Buff();
        public void OnEventListener_Nerf() => player.Nerf();
        public void OnEventListener_Kill() => player.Kill();
        public void OnEventListener_AddLife() => AllServices.PlayerService.AddLife();
        public void OnEventListener_AddCoin() => AllServices.CoinService.Add();
    }
}