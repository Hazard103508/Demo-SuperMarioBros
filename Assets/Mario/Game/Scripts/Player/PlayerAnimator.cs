using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Game.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        #region Events
        [SerializeField] private PlayerController _playerController;
        #endregion

        #region Public Methods
        public void SetFrameId(PlayerAnimationKeys key) => _playerController.CurrentAnimationKey = key;
        #endregion

        #region Structures
        public enum PlayerAnimationKeys
        {
            Run1, 
            Run2, 
            Run3,
            Jump,
            Ducking
        }
        #endregion
    }
}