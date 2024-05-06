using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHolster : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer = null;
    [SerializeField] Transform _transform = null;
    [SerializeField,ReadOnly] WeaponSO _weaponSO = null;

    private void Awake()
    {
        UpdateVisuals();
    }

    public void Init(WeaponSO _newWeaponSO)
    {
        _weaponSO = _newWeaponSO;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (_weaponSO is null)
        {
            _spriteRenderer.sprite = null;
            _transform.localPosition = Vector3.zero;
            _transform.localRotation = Quaternion.identity;
        }
        else
        {
            _spriteRenderer.sprite = _weaponSO.SpriteIcon;
            _transform.localPosition = _weaponSO.HolsterPosition;
            _transform.localRotation = Quaternion.Euler(_weaponSO.HolsterEuler);
        }
    }
}
