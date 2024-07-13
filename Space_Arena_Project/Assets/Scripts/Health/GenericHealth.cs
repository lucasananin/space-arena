using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericHealth : HealthBehaviour
{
    protected override void OnDead_()
    {
        base.OnDead_();
        Destroy(gameObject);
    }
}
