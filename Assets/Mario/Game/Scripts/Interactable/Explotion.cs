using UnityEngine;

namespace Mario.Game.Interactable
{

    public class Explotion : MonoBehaviour
    {
        #region Public Methods
        public void OnAnimationCompleted() => gameObject.SetActive(false);
        #endregion
    }
}