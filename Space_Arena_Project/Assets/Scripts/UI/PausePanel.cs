using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] CanvasGroupView _view = null;

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
        Time.timeScale = Mathf.Epsilon;
    }

    public void Hide()
    {
        _view.Hide();
        Time.timeScale = 1;
    }
}
