using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Environment;
using Mario.Game.ScriptableObjects;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Mario.Application
{
    [InitializeOnLoad]
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
                //ServiceLocator.Current.Get<IGameDataService>(),
                //ServiceLocator.Current.Get<ICoinService>(),
                //ServiceLocator.Current.Get<IScoreService>(),
                ServiceLocator.Current.Get<ITimeService>(),
                //ServiceLocator.Current.Get<ICharacterService>(),
            };

            ServiceLocator.Current.Get<IGameDataService>().MapProfile = _mapProfile;
            Camera.main.backgroundColor = _mapProfile.BackgroundColor; // TEMPORAL........
        }
        private void Update()
        {
            foreach (IGameService service in lstServices)
                service.Update();
        }
    }
}