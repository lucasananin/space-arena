using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractAgent : InteractAgent
{
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
        _currentInteractable = SearchForInteractables();
    }

    private void TryInteract()
    {
        _currentInteractable?.Interact(this);
    }
}
