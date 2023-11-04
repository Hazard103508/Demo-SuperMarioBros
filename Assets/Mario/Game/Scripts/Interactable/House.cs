using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Interactable;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class House : MonoBehaviour, IHittableByPlayerFromLeft
    {
        #region Objects
        private ILevelService _levelService;

        [SerializeField] private HouseProfile _profile;
        #endregion

        #region Private Methods
        private void Awake()
        {
            _levelService = ServiceLocator.Current.Get<ILevelService>();
        }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromLeft(PlayerController player)
        {
            player.gameObject.SetActive(false);
            _levelService.SetNextMap(_profile.Connection);
            _levelService.SetHouseReached();
        }
        #endregion
    }
}