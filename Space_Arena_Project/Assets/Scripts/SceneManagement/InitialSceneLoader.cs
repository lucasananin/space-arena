using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialSceneLoader : MonoBehaviour
{
    [SerializeField] string _sceneName = null;

    private void Start()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
