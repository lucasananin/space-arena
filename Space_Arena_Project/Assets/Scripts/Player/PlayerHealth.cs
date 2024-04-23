using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthBehaviour
{
    public static event System.Action<PlayerHealth> onPlayerDamaged = null;

    protected override void OnDamageTaken()
    {
        onPlayerDamaged?.Invoke(this);
    }

    protected override void OnDead()
    {
        RestoreHealth();
    }
}
