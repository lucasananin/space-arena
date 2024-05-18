using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collectable_Ammo", menuName = "SO/Collectables/Ammo")]
public class CollectableAmmoSO : CollectableSO
{
    public override void Collect(CollectableAgent _agent)
    {
        var _ammoHandler = _agent.GetComponent<AmmoHandler>();
        _ammoHandler.RestoreAmmo();
    }
}
