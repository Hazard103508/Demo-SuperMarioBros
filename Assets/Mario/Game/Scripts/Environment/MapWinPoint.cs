using Mario.Application.Services;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mario.Game.Environment
{
    public class MapWinPoint : MonoBehaviour
    {
        private void Awake()
        {
            AllServices.CharacterService.OnPlayerPositionChanged.AddListener(OnPlayerPositionChanged);
        }
        private void OnDestroy()
        {
            AllServices.CharacterService.OnPlayerPositionChanged.RemoveListener(OnPlayerPositionChanged);
        }
        private void Start()
        {
            if (AllServices.GameDataService.CurrentMapProfile.WinPoint.mapProfile == null)
                Destroy(this);
        }
        public void OnPlayerPositionChanged(Vector3 position)
        {
            if (position.x >= AllServices.GameDataService.CurrentMapProfile.WinPoint.PositionX)
            {
                AllServices.CharacterService.StopMovement();
                AllServices.GameDataService.NextMapProfile = AllServices.GameDataService.CurrentMapProfile.WinPoint.mapProfile;
                AllServices.GameDataService.OnMapCompleted.Invoke();
                AllServices.TimeService.StartTimer();

                StartCoroutine(CloseMap());
            }
        }

        public IEnumerator CloseMap()
        {
            yield return new WaitForSeconds(6);

            if (AllServices.GameDataService.NextMapProfile != null)
            {
                AllServices.GameDataService.CurrentMapProfile = AllServices.GameDataService.NextMapProfile;
                AllServices.GameDataService.NextMapProfile = null;
            }

            SceneManager.LoadScene("StandBy");
        }
    }
}