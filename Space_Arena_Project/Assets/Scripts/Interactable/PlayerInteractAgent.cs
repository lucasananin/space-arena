using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractAgent : InteractAgent
{
    public static event System.Action<InteractableBehaviour> onInteractableChange = null;
    public static event System.Action<InteractableBehaviour> onInteracted = null;

    private void OnEnable()
    {
        InputHandler.OnInteractDown += TryInteract;
    }

    private void OnDisable()
    {
        InputHandler.OnInteractDown -= TryInteract;
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
        if (_currentInteractable is null) return;

        _currentInteractable.Interact(this);
        onInteracted?.Invoke(_currentInteractable);
    }
}
