using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : CollectableBehaviour
{
    public override void OnCollect()
    {
        var _health = _agent.GetComponent<HealthBehaviour>();
        _health.RestoreHealth();
    }
}
