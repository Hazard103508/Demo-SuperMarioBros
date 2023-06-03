using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UILives : MonoBehaviour
    {
        [SerializeField] private IconText label;

        private void Awake()
        {
            OnLivesChanged();
        }
        private void OnEnable() => AllServices.PlayerService.OnLivesAdded.AddListener(OnLivesChanged);
        private void OnDisable() => AllServices.PlayerService.OnLivesAdded.RemoveListener(OnLivesChanged);
        private void OnLivesChanged() => label.Text = AllServices.PlayerService.Lives.ToString();
    }
}
