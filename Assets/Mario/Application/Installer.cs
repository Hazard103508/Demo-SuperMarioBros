using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Environment;
using Mario.Game.ScriptableObjects;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Mario.Application
{
    public class Installer : MonoBehaviour
    {
        [SerializeField] private TargetPoints targetPointsPrefab;
        [SerializeField] private MapProfile _mapProfile; // TEMPORAL

        private List<IGameService> lstServices;

        private void Awake()
        {
            ServiceLocator.Initiailze();
            ServiceLocator.Current.Register<IGameDataService>(new GameDataService());
            ServiceLocator.Current.Register<ICoinService>(new CoinService());
            ServiceLocator.Current.Register<IScoreService>(new ScoreService(targetPointsPrefab));
            ServiceLocator.Current.Register<ITimeService>(new TimeService());
            ServiceLocator.Current.Register<ICharacterService>(new CharacterService());

            lstServices = new List<IGameService>()
            {
                AllServices.TimeService
            };

            AllServices.GameDataService.MapProfile = _mapProfile; // ASIGNAR MAPA EN Escena anterior
            
        }
        private void Update()
        {
            foreach (IGameService service in lstServices)
                service.Update();
        }
    }
}