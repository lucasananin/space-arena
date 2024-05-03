using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractAgent : InteractAgent
{
    public static event System.Action<InteractableBehaviour> onInteractableChange = null;

    private void OnEnable()
    {
        InputHandler.onInteractDown += TryInteract;
    }

    private void OnDisable()
    {
        InputHandler.onInteractDown -= TryInteract;
    }

    private void Update()
    {
        InteractableBehaviour _lastInteractable = _currentInteractable;
        _currentInteractable = SearchForInteractables();

        if (_currentInteractable != _lastInteractable)
        {
            onInteractableChange?.Invoke(_currentInteractable);
        }
    }

    private void TryInteract()
    {
        _currentInteractable?.Interact(this);
    }
}
