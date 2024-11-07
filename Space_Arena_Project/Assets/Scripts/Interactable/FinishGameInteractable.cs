using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishGameInteractable : InteractableBehaviour
{
    [SerializeField] UnityEvent OnShow = null;

    public static event System.Action OnInteracted = null;

    private void OnEnable()
    {
        EnemySpawner.OnEndFinalWave += Show;
    }

    private void OnDisable()
    {
        EnemySpawner.OnEndFinalWave -= Show;
    }

    public override void Interact(InteractAgent _agent)
    {
        base.Interact(_agent);
        OnInteracted?.Invoke();
    }

    private void Show(WaveModel obj)
    {
        OnShow?.Invoke();
    }

    public override string GetText()
    {
        return "Finish Game";
    }
}
