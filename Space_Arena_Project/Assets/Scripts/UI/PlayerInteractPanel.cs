using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteractPanel : MonoBehaviour
{
    [SerializeField] CanvasGroupView _view = null;
    [SerializeField] TextMeshProUGUI _text = null;

    private void OnEnable()
    {
        PlayerInteractAgent.onInteractableChange += UpdateVisuals;
        PlayerInteractAgent.onInteracted += UpdateVisuals;
    }

    private void OnDisable()
    {
        PlayerInteractAgent.onInteractableChange -= UpdateVisuals;
        PlayerInteractAgent.onInteracted -= UpdateVisuals;
    }

    private void UpdateVisuals(InteractableBehaviour _interactableBehaviour)
    {
        if (_interactableBehaviour is null)
        {
            _view.Hide();
        }
        else
        {
            transform.parent.position = _interactableBehaviour.transform.position;
            _view.Show();
            _text.text = $"{_interactableBehaviour.GetText()}";
        }
    }
}
