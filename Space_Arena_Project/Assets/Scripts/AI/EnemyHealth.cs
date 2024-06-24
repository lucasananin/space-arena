using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthBehaviour
{
    public static event System.Action<HealthBehaviour> onAnyAiDead = null;

    protected override void OnDead_()
    {
        base.OnDead_();
        onAnyAiDead?.Invoke(this);
        gameObject.SetActive(false);
    }
}
