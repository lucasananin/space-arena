using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthBehaviour
{
    [SerializeField] float _deathDelay = 0f;

    public static event System.Action<HealthBehaviour> OnAnyAiDead = null;

    protected override void OnDead_()
    {
        base.OnDead_();

        if (_deathDelay > 0)
        {
            StartCoroutine(Dead_routine());
        }
        else
        {
            OnAnyAiDead?.Invoke(this);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator Dead_routine()
    {
        yield return new WaitForSeconds(_deathDelay);
        OnAnyAiDead?.Invoke(this);
        gameObject.SetActive(false);
    }
}
