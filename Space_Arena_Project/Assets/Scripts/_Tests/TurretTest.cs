using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTest : MonoBehaviour
{
    [SerializeField] WeaponBehaviour _weaponBehaviour = null;
    [SerializeField] float _fireRate = 1f;
    [SerializeField, ReadOnly] float _nextFire = 0f;

    private void Update()
    {
        _nextFire += Time.deltaTime;

        if (_nextFire > _fireRate)
        {
            _nextFire = 0;
            _weaponBehaviour.Shoot();
        }
    }
}
