using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UIPlayer : MonoBehaviour
    {
        private IPlayerService _playerService;
        [SerializeField] private IconText label;


        private void Awake()
        {
            _playerService = ServiceLocator.Current.Get<IPlayerService>();

        }
        private void Start()
        {
            label.Text = _playerService.PlayerProfile.PlayerName;
        }
    }
}
