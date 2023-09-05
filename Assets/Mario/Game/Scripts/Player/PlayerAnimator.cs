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
        public void OnFrameChange(int frame) => _playerController.StateMachine.CurrentState.CurrentAnimationFrame = frame;
        #endregion
    }
}