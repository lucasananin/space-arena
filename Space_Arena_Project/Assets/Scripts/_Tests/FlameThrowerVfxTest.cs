using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerVfxTest : MonoBehaviour
{
    [SerializeField] WeaponBehaviour _weaponBehaviour = null;
    //[SerializeField] WeaponFlipper _weaponFlipper = null;
    [SerializeField] ParticleSystem _particleSystem = null;
    [Space]
    [SerializeField] Transform _muzzle = null;
    [SerializeField] Transform _collisionPlane = null;
    [SerializeField] LayerMask _obstacleLayerMask = default;
    [SerializeField] float _detectionRadius = 1f;

    private RaycastHit2D[] _results = new RaycastHit2D[9];

    private void Start()
    {
        _particleSystem.transform.parent = null;
        _collisionPlane.transform.parent = null;
    }

    private void OnEnable()
    {
        _weaponBehaviour.OnPullTrigger += Play;
        _weaponBehaviour.OnReleaseTrigger += Stop;
        //_weaponFlipper.onFlip += Restart;
    }

    private void OnDisable()
    {
        _weaponBehaviour.OnPullTrigger -= Play;
        _weaponBehaviour.OnReleaseTrigger -= Stop;
        //_weaponFlipper.onFlip -= Restart;
    }

    // Se acabar a municao e estiver tocando, Stop();

    private void LateUpdate()
    {
        _particleSystem.transform.position = _muzzle.position;
        _particleSystem.transform.rotation = _weaponBehaviour.transform.rotation;

        RotatePlane();
        _collisionPlane.position = CalculatePlanePosition();
    }

    private Vector3 CalculatePlanePosition()
    {
        var _maxDistance = _weaponBehaviour.GetCastProjectileMaxDistance();
        var _hits = Physics2D.CircleCastNonAlloc(_muzzle.position, _detectionRadius, _muzzle.right, _results, _maxDistance, _obstacleLayerMask);

        if (_hits <= 0)
        {
            return transform.position + _muzzle.right * (_maxDistance * 2f);
        }
        else
        {
            return _results[0].point;
        }
    }

    private void RotatePlane()
    {
        var _rotation = GeneralMethods.GetLookRotation(_collisionPlane.position, _muzzle.position);
        var _offset = Quaternion.Euler(Vector3.forward * -90f);
        _collisionPlane.rotation = _rotation * _offset;
    }

    private void Play()
    {
        if (!_weaponBehaviour.HasAmmo()) return;

        _particleSystem.Play();
    }

    private void Stop()
    {
        _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private void Restart()
    {
        if (!_particleSystem.isPlaying) return;
        Stop();
        Play();
    }
}
