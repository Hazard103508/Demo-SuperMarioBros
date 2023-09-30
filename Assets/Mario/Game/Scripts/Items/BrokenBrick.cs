using UnityEngine;

namespace Mario.Game.Items
{
    public class BrokenBrick : MonoBehaviour
    {
        #region Public Methods
        public void OnAnimationCompleted() => gameObject.SetActive(false);
        #endregion
    }
}