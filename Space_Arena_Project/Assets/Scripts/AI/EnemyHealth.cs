using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthBehaviour
{
    public static event System.Action<HealthBehaviour> onAnyAiDead = null;

    protected override void OnDamageTaken()
    {
        //
    }

    protected override void OnDead()
    {
        gameObject.SetActive(false);
        onAnyAiDead?.Invoke(this);
    }
}
