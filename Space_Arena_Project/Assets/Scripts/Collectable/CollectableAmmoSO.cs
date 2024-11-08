using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collectable_Ammo", menuName = "SO/Collectables/Ammo")]
public class CollectableAmmoSO : CollectableSO
{
    [SerializeField, Range(0, 100)] int _minRestorePercentage = 8;
    [SerializeField, Range(0, 100)] int _maxRestorePercentage = 12;
    [SerializeField] AmmoRestore _restoreType = AmmoRestore.ChooseOne;

    public override void Collect(CollectableAgent _agent)
    {
        var _ammoHandler = _agent.GetComponent<AmmoHandler>();
        var _percentage = Random.Range(_minRestorePercentage, _maxRestorePercentage);

        if (_restoreType == AmmoRestore.ChooseOne)
        {
            _ammoHandler.RestoreOneAmmoModel(_percentage);
        }
        else
        {
            _ammoHandler.RestoreAmmo(_percentage);
        }
    }

    enum AmmoRestore
    {
        ChooseOne,
        RestoreAll,
    }
}
