using UnityEngine;
using UnityEngine.Events;

namespace UnityShared.Animations.SceneTransitions
{
    [RequireComponent(typeof(Animator))]
    public class SceneTransition : MonoBehaviour
    {
        [HideInInspector] public Animator animator;
        [HideInInspector] public UnityEvent onOpeningScene;
        [HideInInspector] public UnityEvent onOpenedScene;
        [HideInInspector] public UnityEvent onClosingScene;
        [HideInInspector] public UnityEvent onClosedScene;

        private void OnEnable()
        {
            animator = GetComponent<Animator>();
        }

        public void OnOpening() => onOpeningScene.Invoke();
        public void OnOpened() => onOpenedScene.Invoke();
        public void OnClosing() => onClosingScene.Invoke();
        public void OnClosed() => onClosedScene.Invoke();
    }
}