using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthBehaviour
{
    [SerializeField] ParticleSystem _deadVfx = null;

    public static event System.Action<HealthBehaviour> onAnyAiDead = null;

    protected override void OnDamageTaken()
    {
        //
    }

    protected override void OnDead()
    {
        Instantiate(_deadVfx, transform.position, transform.rotation);
        gameObject.SetActive(false);
        onAnyAiDead?.Invoke(this);
    }
}
