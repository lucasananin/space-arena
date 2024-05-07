using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastLineVfxSpawner : MonoBehaviour
{
    [SerializeField] CastProjectile _castProjectile = null;
    [SerializeField] CastLineVfx _lineVfx = null;

    private void OnEnable()
    {
        _castProjectile.onCastEnd += SpawnVfx;
    }

    private void OnDisable()
    {
        _castProjectile.onCastEnd += SpawnVfx;
    }

    private void SpawnVfx(List<RaycastHit2D> _pointsHit)
    {
        Vector3 _myPosition = transform.position;
        CastLineVfx _instance = Instantiate(_lineVfx, _myPosition, transform.rotation);

        if (_pointsHit.Count > 0)
        {
            RaycastHit2D _lastPoint = _pointsHit[^1];
            _instance.Init(_lastPoint.point);
        }
        else
        {
            Vector3 _point = _myPosition + (transform.right * _castProjectile.GetCastMaxDistance());
            _instance.Init(_point);
        }
    }
}
