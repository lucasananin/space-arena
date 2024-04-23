using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthBehaviour
{
    protected override void OnDamageTaken()
    {
        //
    }

    protected override void OnDead()
    {
        gameObject.SetActive(false);
    }
}
