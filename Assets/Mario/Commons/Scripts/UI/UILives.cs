using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UILives : MonoBehaviour
    {
        [SerializeField] private IconText label;

        private void Awake()
        {
            AllServices.PlayerService.OnLivesAdded.AddListener(OnLivesChanged);
            OnLivesChanged();
        }
        private void OnDestroy() => AllServices.PlayerService.OnLivesAdded.RemoveListener(OnLivesChanged);
        private void OnLivesChanged() => label.Text = AllServices.PlayerService.Lives.ToString();
    }
}
