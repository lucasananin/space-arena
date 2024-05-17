using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollectable : CollectableBehaviour
{
    public override void OnCollect()
    {
        var _ammoHandler = _agent.GetComponent<AmmoHandler>();
        _ammoHandler.RestoreAmmo();
    }
}
