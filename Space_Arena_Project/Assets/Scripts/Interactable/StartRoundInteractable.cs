using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoundInteractable : InteractableBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer = null;
    [SerializeField] Collider2D _collider = null;

    public static event System.Action OnInteracted = null;

    private void OnEnable()
    {
        EnemySpawner.OnEndWave += Show;
        EnemySpawner.OnEndWaveGroupChanged += Show;
        EnemySpawner.OnEndFinalWave += Disable;
    }

    private void OnDisable()
    {
        EnemySpawner.OnEndWave -= Show;
        EnemySpawner.OnEndWaveGroupChanged -= Show;
        EnemySpawner.OnEndFinalWave -= Disable;
    }

    public override void Interact(InteractAgent _agent)
    {
        base.Interact(_agent);
        OnInteracted?.Invoke();
        Hide();
    }

    public override string GetText()
    {
        return $"Start Round";
    }

    private void Show(WaveSO _so)
    {
        _spriteRenderer.enabled = true;
        _collider.enabled = true;
    }

    private void Show(WaveModel _wave)
    {
        _spriteRenderer.enabled = true;
        _collider.enabled = true;
    }

    private void Hide()
    {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
    }

    private void Disable(WaveModel obj)
    {
        gameObject.SetActive(false);
    }
}
