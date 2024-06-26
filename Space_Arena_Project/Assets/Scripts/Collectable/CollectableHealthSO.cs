using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collectable_Health", menuName = "SO/Collectables/Health")]
public class CollectableHealthSO : CollectableSO
{
    [SerializeField, Range(0, 100)] int _restorePercentage = 10;

    public override void Collect(CollectableAgent _agent)
    {
        var _health = _agent.GetComponent<HealthBehaviour>();
        _health.RestoreHealth(_restorePercentage);
    }
}
