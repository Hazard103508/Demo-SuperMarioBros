using Mario.Application.Services;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class LifeNotifiction1Down : MonoBehaviour
    {
        [SerializeField] private AudioSource _1DownFX;

        private void Awake() => AllServices.PlayerService.OnLivesRemoved.AddListener(OnLivesRemoved);
        private void OnDestroy() => AllServices.PlayerService.OnLivesRemoved.RemoveListener(OnLivesRemoved);

        private void OnLivesRemoved()
        {
            AllServices.TimeService.StopTimer();
            AllServices.PlayerService.CanMove = false;

            _1DownFX.Play();
            StartCoroutine(ReloadMap());
        }
        private IEnumerator ReloadMap()
        {
            yield return new WaitForSeconds(3.5f);

            if (AllServices.PlayerService.Lives == 0)
                AllServices.SceneService.LoadGameOverScene();
            else if (AllServices.TimeService.Time == 0)
                AllServices.SceneService.LoadTimeUpScene();
            else
                AllServices.SceneService.LoadStandByScene();
        }
    }
}