using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGameButton : MonoBehaviour
{
    [SerializeField] Button _button = null;

    private void OnValidate()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Quit);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    public void Quit()
    {
        _button.interactable = false;
        Application.Quit();
    }
}
