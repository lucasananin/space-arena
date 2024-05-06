using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInteractable : InteractableBehaviour
{
    [SerializeField] WeaponLoot _weaponLoot = null;
    [SerializeField, ReadOnly] WeaponSO _weaponSO = null;

    private void OnEnable()
    {
        _weaponLoot.onInit += UpdateValues;
    }

    private void OnDisable()
    {
        _weaponLoot.onInit -= UpdateValues;
    }

    private void UpdateValues()
    {
        _weaponSO = _weaponLoot.WeaponSO;
    }

    public override void Interact(InteractAgent _agent)
    {
        var _weaponHandler = _agent.GetComponent<PlayerWeaponHandler>();

        if (!_weaponHandler.HasWeapon(_weaponSO))
        {
            _weaponHandler.AddWeapon(_weaponSO, out WeaponSO _droppedWeaponSO);

            if (_droppedWeaponSO is null)
                Destroy(gameObject);
            else
                _weaponLoot.Init(_droppedWeaponSO);
        }
        else
        {
            Debug.Log($"// You already have this weapon!");
        }
    }

    public override string GetText()
    {
        return _weaponSO.DisplayName;
    }
}
