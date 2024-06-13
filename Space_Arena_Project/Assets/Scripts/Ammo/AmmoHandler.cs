using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoHandler : MonoBehaviour
{
    [SerializeField] bool _infiniteAmmo = false;
    [SerializeField] AmmoModel[] _ammoTypes = null;

    private void Start()
    {
        RestoreAmmo();
    }

    [Button]
    public void RestoreAmmo()
    {
        int _count = _ammoTypes.Length;

        for (int i = 0; i < _count; i++)
        {
            _ammoTypes[i].RestoreQuantity();
        }
    }

    public void DecreaseAmmo(ProjectileSO _projectileSO, WeaponSO _weaponSO)
    {
        if (_infiniteAmmo) return;

        var _model = GetModel(_projectileSO.AmmoSO);
        _model?.DecreaseQuantity(_weaponSO.AmmoPerShot);
    }

    public bool HasAmmo(ProjectileSO _projectileSO, WeaponSO _weaponSO)
    {
        var _model = GetModel(_projectileSO.AmmoSO);
        return _model is not null && _model.HasEnoughQuantity(_weaponSO.AmmoPerShot);
    }

    private AmmoModel GetModel(AmmoSO _ammoSO)
    {
        int _count = _ammoTypes.Length;

        for (int i = 0; i < _count; i++)
        {
            var _model = _ammoTypes[i];
            var _isTheSameId = GeneralMethods.IsTheSameString(_ammoSO.Id, _model.GetId());

            if (_isTheSameId)
            {
                return _model;
            }
        }

        return null;
    }
}
