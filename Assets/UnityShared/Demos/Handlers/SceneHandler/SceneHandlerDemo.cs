using UnityEngine;
using UnityShared.Behaviours.Handlers;

namespace UnityShared.Demos.Handlers
{
    public class SceneHandlerDemo : MonoBehaviour
    {
        public void OnButtonStandarClick() => SceneHandler.Instance.LoadScene("SceneHandlerDemo2", Enums.LoadSceneBehaviour.STANDARD);
        public void OnButtonAsyncClick() => SceneHandler.Instance.LoadScene("SceneHandlerDemo2", Enums.LoadSceneBehaviour.ASYNC);

        public void OnButtonGoback() => SceneHandler.Instance.GoBack(Enums.LoadSceneBehaviour.STANDARD);
        public void OnButtonRestart() => SceneHandler.Instance.Restart(Enums.LoadSceneBehaviour.STANDARD);

    }
}