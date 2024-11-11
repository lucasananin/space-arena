using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
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

        OnEndLoad?.Invoke();
    }
}
