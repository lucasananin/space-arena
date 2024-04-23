using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowHealth : HealthBehaviour
{
    protected override void OnDamageTaken()
    {
        RestoreHealth();
    }

    protected override void OnDead()
    {
        //
    }
}
