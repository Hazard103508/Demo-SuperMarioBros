using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UILives : MonoBehaviour
    {
        [SerializeField] private IconText label;

        private void Awake()
        {
            Services.PlayerService.OnLivesAdded.AddListener(OnLivesChanged);
            OnLivesChanged();
        }
        private void OnDestroy() => Services.PlayerService.OnLivesAdded.RemoveListener(OnLivesChanged);
        private void OnLivesChanged() => label.Text = Services.PlayerService.Lives.ToString();
    }
}
