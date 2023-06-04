using Mario.Application.Services;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mario.Game.Environment
{
    public class MapEndPoint : MonoBehaviour
    {
        private void Awake()
        {
            AllServices.PlayerService.OnPositionChanged.AddListener(OnPlayerPositionChanged);
        }
        private void OnDestroy()
        {
            AllServices.PlayerService.OnPositionChanged.RemoveListener(OnPlayerPositionChanged);
        }
        private void Start()
        {
            if (!AllServices.GameDataService.CurrentMapProfile.EndPoint.HasEndPoint)
                Destroy(this);
        }
        public void OnPlayerPositionChanged(Vector3 position)
        {
            if (position.x >= AllServices.GameDataService.CurrentMapProfile.EndPoint.PositionX)
            {
                AllServices.PlayerService.CanMove = false;
                AllServices.GameDataService.NextMapProfile = AllServices.GameDataService.CurrentMapProfile.EndPoint.mapProfile;
                //AllServices.GameDataService.OnGoalReached.Invoke();
                AllServices.TimeService.StartTimer();

                StartCoroutine(CloseMap());
            }
        }

        public IEnumerator CloseMap()
        {
            yield return new WaitForSeconds(6);
            SceneManager.LoadScene("StandBy");
        }
    }
}