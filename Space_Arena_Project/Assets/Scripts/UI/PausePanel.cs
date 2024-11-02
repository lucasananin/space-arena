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
                _view.Hide();
                Time.timeScale = 1;
            }
            else
            {
                _view.Show();
                Time.timeScale = Mathf.Epsilon;
            }
        }
    }
}
