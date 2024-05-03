using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitchInteractable : InteractableBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer = null;

    public override void Interact(InteractAgent _agent)
    {
        _spriteRenderer.color = Random.ColorHSV();
    }
}
