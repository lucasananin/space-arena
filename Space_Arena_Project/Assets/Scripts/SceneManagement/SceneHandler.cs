using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] float _loadDelay = 1f;

    public static event Action OnStartLoad = null;
    public static event Action OnEndLoad = null;

    public void LoadScene(string _sceneToLoad, string _sceneToUnload)
    {
        Time.timeScale = 1;
        StartCoroutine(Load_routine(_sceneToLoad, _sceneToUnload));
    }

    IEnumerator Load_routine(string _sceneToLoad, string _sceneToUnload)
    {
        OnStartLoad?.Invoke();

        yield return new WaitForSeconds(_loadDelay);

        if (!string.IsNullOrEmpty(_sceneToUnload))
        {
            AsyncOperation _asyncUnload = SceneManager.UnloadSceneAsync(_sceneToUnload);

            while (!_asyncUnload.isDone)
            {
                yield return null;
            }
        }

        AsyncOperation _asyncLoad = SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Additive);

        while (!_asyncLoad.isDone)
        {
            yield return null;
        }

        var _scene = SceneManager.GetSceneByName(_sceneToLoad);
        SceneManager.SetActiveScene(_scene);

        yield return new WaitForSeconds(_loadDelay);

        OnEndLoad?.Invoke();
    }
}
