using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoundInteractable : InteractableBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer = null;
    [SerializeField] Collider2D _collider = null;

    public static event System.Action onInteracted = null;

    private void OnEnable()
    {
        EnemySpawner.onEnd += Show;
    }

    private void OnDisable()
    {
        EnemySpawner.onEnd += Show;
    }

    public override void Interact(InteractAgent _agent)
    {
        onInteracted?.Invoke();
        Hide();
    }

    public override string GetText()
    {
        return $"Start Round";
    }

    private void Show()
    {
        _spriteRenderer.enabled = true;
        _collider.enabled = true;
    }

    private void Hide()
    {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
    }
}
