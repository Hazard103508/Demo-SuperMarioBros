using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Environment;
using Mario.Game.ScriptableObjects.Map;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityShared.Behaviours.Handlers;

namespace Mario.Application
{
    public class Installer : MonoBehaviour
    {
        [SerializeField] private TargetPoints targetPointsPrefab;

        [Header("--TEMPORAL--")]
        [SerializeField] private MapProfile _mapProfile; // TEMPORAL
        [SerializeField] private PlayerProfile _playerProfile; // TEMPORAL

        private void Start()
        {
            ServiceLocator.Initiailze();
            ServiceLocator.Current.Register<IGameDataService>(new GameDataService());
            ServiceLocator.Current.Register<ICoinService>(new CoinService());
            ServiceLocator.Current.Register<IScoreService>(new ScoreService(targetPointsPrefab));
            ServiceLocator.Current.Register<ITimeService>(new TimeService());
            ServiceLocator.Current.Register<ICharacterService>(new CharacterService());
            ServiceLocator.Current.Register<ILifeService>(new LifeService());

            AllServices.Load();
            AllServices.GameDataService.MapProfile = _mapProfile; // ASIGNAR MAPA EN Escena anterior
            AllServices.GameDataService.PlayerProfile = _playerProfile;

            SceneManager.LoadScene("StandBy", LoadSceneMode.Single);
        }
    }
}