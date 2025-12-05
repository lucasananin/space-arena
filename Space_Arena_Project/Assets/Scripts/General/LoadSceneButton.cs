using UnityEngine;
using UnityEngine.UI;

public class LoadSceneButton : SceneLoader
{
    [SerializeField] Button _button = null;

    private void OnValidate()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Restart);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    public void Restart()
    {
        _button.interactable = false;
        LoadScene();
    }
}
