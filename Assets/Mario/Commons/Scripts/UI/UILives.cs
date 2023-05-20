using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UILives : MonoBehaviour
    {
        [SerializeField] private TextGenerator label;

        private void Awake()
        {
            OnLivesChanged();
        }
        private void OnEnable() => AllServices.LifeService.OnLivesAdded.AddListener(OnLivesChanged);
        private void OnDisable() => AllServices.LifeService.OnLivesAdded.RemoveListener(OnLivesChanged);
        private void OnLivesChanged() => label.Text = AllServices.LifeService.Lives.ToString();
    }
}
