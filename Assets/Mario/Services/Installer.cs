using Mario.Game.ScriptableObjects;
using Mario.Services.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Mario.Services
{
    [InitializeOnLoadAttribute]
    public class Installer : MonoBehaviour
    {
        [SerializeField] private GameDataProfile _gameDataProfile;

        private void Awake()
        {
            ServiceLocator.Initiailze();
            ServiceLocator.Current.Register<ICoinService>(new CoinService(_gameDataProfile));
        }
    }
}