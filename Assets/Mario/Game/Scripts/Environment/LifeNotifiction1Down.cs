using Mario.Application.Services;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mario.Game.Environment
{
    public class LifeNotifiction1Down : MonoBehaviour
    {
        [SerializeField] private AudioSource _1DownFX;

        private void OnEnable() => AllServices.LifeService.OnLivesRemoved.AddListener(OnLivesRemoved);
        private void OnDisable() => AllServices.LifeService.OnLivesRemoved.RemoveListener(OnLivesRemoved);

        private void OnLivesRemoved()
        {
            AllServices.TimeService.StopTimer();
            AllServices.CharacterService.StopMovement();

            _1DownFX.Play();
            StartCoroutine(ReloadMap());
        }
        private IEnumerator ReloadMap()
        {
            yield return new WaitForSeconds(3.5f);
            
            string _nextScene =
                AllServices.LifeService.Lives == 0 ? "GameOver" :
                AllServices.TimeService.Time == 0 ? "TimeUp" :
                "StandBy";

            SceneManager.LoadScene(_nextScene);
        }
    }
}