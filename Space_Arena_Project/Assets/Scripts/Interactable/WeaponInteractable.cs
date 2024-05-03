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
        // pega o weaponhandler.
        // adiciona na lista.

        Debug.Log($"// Weapon taken!");
        Destroy(gameObject);
    }

    public override string GetText()
    {
        return _weaponSO.DisplayName;
    }
}
