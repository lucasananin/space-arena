using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHitVfxSpawner : MonoBehaviour
{
    [SerializeField] ParticleSystem _hitVfx = null;

    public void SpawnVfx(RaycastHit2D _hitInfo)
    {
        var _i = Instantiate(_hitVfx, _hitInfo.point, Quaternion.identity);
        _i.transform.right = _hitInfo.normal;
    }
}
