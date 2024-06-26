using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthBehaviour
{
    public static event System.Action<PlayerHealth> OnPlayerDamaged = null;

    protected override void OnDamageTaken_()
    {
        base.OnDamageTaken_();
        OnPlayerDamaged?.Invoke(this);
    }

    protected override void OnDead_()
    {
        //RestoreHealth();
        gameObject.SetActive(false);
    }
}
