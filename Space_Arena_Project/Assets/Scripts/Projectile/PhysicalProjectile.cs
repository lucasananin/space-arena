using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalProjectile : AbstractProjectileBehaviour
{
    [SerializeField] float _moveSpeed = 20f;

    private void Update()
    {
        transform.position += transform.right * _moveSpeed  * Time.deltaTime;
    }

    public override void Init(AbstractWeaponBehaviour _weapon)
    {
        throw new System.NotImplementedException();
    }
}
