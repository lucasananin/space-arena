using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectileMod : MonoBehaviour
{
    [SerializeField] ProjectileBehaviour _projectileBehaviour = null;

    private void OnEnable()
    {
        _projectileBehaviour.onRaycastHit += Explode;
    }

    private void OnDisable()
    {
        _projectileBehaviour.onRaycastHit -= Explode;
    }

    private void Explode(RaycastHit2D _raycastHit)
    {
        _projectileBehaviour.Explode(_raycastHit.point);
        // spawna o vfx.
    }
}
