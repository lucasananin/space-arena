using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoLoot : LootBehaviour
{
    [SerializeField, ReadOnly] AmmoSO _ammoSO = null;

    public override void Init(ScriptableObject _soValue)
    {
        _ammoSO = _soValue as AmmoSO;
        SendOnInit();
    }
}
