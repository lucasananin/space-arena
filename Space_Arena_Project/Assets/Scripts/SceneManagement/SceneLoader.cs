using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string _sceneToLoad = null;
    [SerializeField] string _sceneToUnload = null;
    [SerializeField] bool _loadOnStart = false;

    private void Start()
    {
        if (_loadOnStart)
        {
            LoadScene();
        }
    }

    public void LoadScene()
    {
        var _sceneHandler = FindObjectOfType<SceneHandler>();
        _sceneHandler.LoadScene(_sceneToLoad, _sceneToUnload);
    }
}
