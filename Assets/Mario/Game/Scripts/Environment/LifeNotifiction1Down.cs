using Mario.Application.Services;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mario.Game.Environment
{
    public class LifeNotifiction1Down : MonoBehaviour
    {
        [SerializeField] private AudioSource _1DownFX;

        private void OnEnable() => AllServices.PlayerService.OnLivesRemoved.AddListener(OnLivesRemoved);
        private void OnDisable() => AllServices.PlayerService.OnLivesRemoved.RemoveListener(OnLivesRemoved);

        private void OnLivesRemoved()
        {
            AllServices.TimeService.StopTimer();
            AllServices.ItemsService.StopMovement();

            _1DownFX.Play();
            StartCoroutine(ReloadMap());
        }
        private IEnumerator ReloadMap()
        {
            yield return new WaitForSeconds(3.5f);
            
            string _nextScene =
                AllServices.PlayerService.Lives == 0 ? "GameOver" :
                AllServices.TimeService.Time == 0 ? "TimeUp" :
                "StandBy";

            SceneManager.LoadScene(_nextScene);
        }
    }
}