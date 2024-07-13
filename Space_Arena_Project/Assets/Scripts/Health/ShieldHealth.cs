using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHealth : HealthBehaviour
{
    protected override void OnDead_()
    {
        base.OnDead_();
        RestoreHealth();
    }
}
