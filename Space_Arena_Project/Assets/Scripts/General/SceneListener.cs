using UnityEngine;
using UnityEngine.Events;

public class SceneListener : MonoBehaviour
{
    [SerializeField] string _sceneName = null;
    [SerializeField] UnityEvent _onLoaded = null;
    [SerializeField] UnityEvent _onUnloaded = null;

    private void OnEnable()
    {
        SceneHandler.OnSceneLoaded += SceneHandler_OnSceneLoaded;
        SceneHandler.OnSceneUnloaded += SceneHandler_OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneHandler.OnSceneLoaded -= SceneHandler_OnSceneLoaded;
        SceneHandler.OnSceneUnloaded -= SceneHandler_OnSceneUnloaded;
    }

    private void SceneHandler_OnSceneLoaded(string _sceneName)
    {
        if (this._sceneName == _sceneName)
        {
            _onLoaded?.Invoke();
        }
    }

    private void SceneHandler_OnSceneUnloaded(string _sceneName)
    {
        if (this._sceneName == _sceneName)
        {
            _onUnloaded?.Invoke();
        }
    }
}
