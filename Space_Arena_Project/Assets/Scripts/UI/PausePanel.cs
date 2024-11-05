using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] CanvasGroupView _view = null;

    public static event System.Action OnPause = null;
    public static event System.Action OnUnpause = null;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDead += Disable;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDead -= Disable;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_view.IsVisible())
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }

    public void Show()
    {
        _view.Show();
        OnPause?.Invoke();
        Time.timeScale = Mathf.Epsilon;
    }

    public void Hide()
    {
        _view.Hide();
        OnUnpause?.Invoke();
        Time.timeScale = 1;
    }

    private void Disable(PlayerHealth _health)
    {
        gameObject.SetActive(false);
    }
}
