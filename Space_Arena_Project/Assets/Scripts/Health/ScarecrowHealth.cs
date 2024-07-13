using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowHealth : HealthBehaviour
{
    protected override void OnDamageTaken_()
    {
        base.OnDamageTaken_();
        RestoreHealth();
    }
}
