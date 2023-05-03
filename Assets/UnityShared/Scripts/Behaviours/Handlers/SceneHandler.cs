using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityShared.Animations.SceneTransitions;
using UnityShared.Enums;
using UnityShared.Patterns;

namespace UnityShared.Behaviours.Handlers
{
    public class SceneHandler : Singleton<SceneHandler>
    {
        #region Objects
        public SceneTransition transition;
        [HideInInspector] public UnityEvent<Scene> onSceneLoading;
        [HideInInspector] public UnityEvent<Scene> onSceneLoaded;
        [HideInInspector] public UnityEvent<Scene> onSceneUnloading;
        [HideInInspector] public UnityEvent<Scene> onSceneUnloaded;

        private string _previousSceneName;
        private string _nextSceneName;
        private LoadSceneBehaviour _behaviour;
        #endregion

        #region Properties
        public string CurrentSceneName => SceneManager.GetActiveScene().name;
        #endregion


        #region Awake
        protected override void Awake()
        {
            base.Awake();

            transition.onOpenedScene.AddListener(OnLoading);
            transition.onOpenedScene.AddListener(OnLoaded);
            transition.onClosingScene.AddListener(OnUnloading);
            transition.onClosedScene.AddListener(OnUnloaed);
        }
        #endregion


        #region Public Methods
        public void LoadScene(string sceneName, LoadSceneBehaviour behaviour)
        {
            _previousSceneName = CurrentSceneName;
            _nextSceneName = sceneName;
            _behaviour = behaviour;

            transition.gameObject.SetActive(true);
            transition.animator.SetTrigger("Unload");
        }
        public void GoBack(LoadSceneBehaviour behaviour)
        {
            if (!string.IsNullOrWhiteSpace(_previousSceneName))
                LoadScene(_previousSceneName, behaviour);
        }
        public void Restart(LoadSceneBehaviour behaviour) => LoadScene(CurrentSceneName, behaviour);
        #endregion

        #region Private Methods
        private void OnLoading()
        {
            onSceneLoading.Invoke(SceneManager.GetActiveScene());
        }
        private void OnLoaded()
        {
            transition.gameObject.SetActive(false);
            onSceneLoaded.Invoke(SceneManager.GetActiveScene());
        }
        private void OnUnloading()
        {
            onSceneUnloading.Invoke(SceneManager.GetActiveScene());
        }
        private void OnUnloaed()
        {
            if (_behaviour == LoadSceneBehaviour.STANDARD)
            {
                SceneManager.LoadScene(_nextSceneName);
                transition.animator.SetTrigger("Load");
            }
            else if (_behaviour == LoadSceneBehaviour.ASYNC)
            {
                var _op = SceneManager.LoadSceneAsync(_nextSceneName);
                _op.completed += op => transition.animator.SetTrigger("Load");
            }

            onSceneUnloaded.Invoke(SceneManager.GetActiveScene());
        }
        #endregion
    }
}