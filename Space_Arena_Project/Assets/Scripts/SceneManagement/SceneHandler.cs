using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    //private string _sceneToLoad = null;
    //private string _sceneToUnload = null;

    public void LoadScene(string _sceneToLoad, string _sceneToUnload)
    {
        //this._sceneToLoad = _sceneToLoad;
        //this._sceneToUnload = _sceneToUnload;
        StartCoroutine(Load_routine(_sceneToLoad, _sceneToUnload));
    }

    IEnumerator Load_routine(string _sceneToLoad, string _sceneToUnload)
    {
        if (!string.IsNullOrEmpty(_sceneToUnload))
        {
            AsyncOperation _asyncUnload = SceneManager.LoadSceneAsync(_sceneToUnload);

            while (!_asyncUnload.isDone)
            {
                yield return null;
            }
        }

        AsyncOperation _asyncLoad = SceneManager.LoadSceneAsync(_sceneToLoad);

        while (!_asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
