using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoHandler : MonoBehaviour
{
    [SerializeField] bool _infiniteAmmo = false;
    [SerializeField] AmmoModel[] _models = null;

    public AmmoModel[] Models { get => _models; private set => _models = value; }

    public event System.Action OnAmmoChanged = null;

    private void Awake()
    {
        RestoreAllAmmo();
    }

    [Button]
    public void RestoreAllAmmo()
    {
        RestoreAllAmmo(999);
    }

    public void RestoreAllAmmo(int _percentage)
    {
        int _count = _models.Length;

        for (int i = 0; i < _count; i++)
        {
            _models[i].RestoreQuantity(_percentage);
        }

        OnAmmoChanged?.Invoke();
    }

    public void DecreaseAmmo(ProjectileSO _projectileSO, WeaponSO _weaponSO)
    {
        if (_infiniteAmmo) return;

        var _model = GetModel(_projectileSO.AmmoSO);
        _model?.DecreaseQuantity(_weaponSO.AmmoPerShot);
        OnAmmoChanged?.Invoke();
    }

    public bool HasAmmo(ProjectileSO _projectileSO, WeaponSO _weaponSO)
    {
        var _model = GetModel(_projectileSO.AmmoSO);
        return _model is not null && _model.HasEnoughQuantity(_weaponSO.AmmoPerShot);
    }

    public int GetAmmoQuantity(ProjectileSO _projectileSO)
    {
        var _model = GetModel(_projectileSO.AmmoSO);
        return _model is not null ? _model.Quantity : -1;
    }

    private AmmoModel GetModel(AmmoSO _ammoSO)
    {
        int _count = _models.Length;

        for (int i = 0; i < _count; i++)
        {
            var _model = _models[i];
            var _isTheSameId = GeneralMethods.IsTheSameString(_ammoSO.Id, _model.GetId());

            if (_isTheSameId)
            {
                return _model;
            }
        }

        return null;
    }
}
