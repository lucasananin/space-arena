using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoHandler : MonoBehaviour
{
    [SerializeField] AmmoModel[] _ammoTypes = null;

    private void Start()
    {
        RestoreAmmo();
    }

    private void RestoreAmmo()
    {
        int _count = _ammoTypes.Length;

        for (int i = 0; i < _count; i++)
        {
            _ammoTypes[i].RestoreQuantity();
        }
    }
}
