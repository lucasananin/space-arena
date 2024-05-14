using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiWeaponHandler : MonoBehaviour
{
    [SerializeField] EntityBehaviour _entitySource = null;
    [SerializeField] WeaponBehaviour _currentWeapon = null;
    [SerializeField] WeaponRotator _weaponRotator = null;

    [Title("// Debug")]
    [SerializeField, ReadOnly] bool _canShoot = true;

    public event System.Action onStoppedShooting = null;

    public bool CanShoot { get => _canShoot; private set => _canShoot = value; }

    private void Awake()
    {
        _canShoot = true;
        _currentWeapon.Init(_entitySource);
    }

    public void StartShooting()
    {
        if (!_canShoot) return;

        StartCoroutine(StartShooting_routine());
    }

    public void RotateWeapon(Vector3 _position)
    {
        _weaponRotator.LookAtPosition(_position);
    }

    public void ResetWeaponRotation()
    {
        _weaponRotator.ResetRotation();
    }

    // Se o inimigo possuir um "action" que permita a troca de armas, 
    // vai ser preciso fazer ela esperar o _canShoot
    // ou dar um stopCoroutine e resetar o _canShoot ao fazer a troca.
    private IEnumerator StartShooting_routine()
    {
        _canShoot = false;
        yield return null;

        _currentWeapon.PullTrigger();
        float _waitTime = _currentWeapon.GetPullTriggerTotalTime();
        //Debug.Log($"// GetPullTriggerTotalTime = {_waitTime}");
        yield return new WaitForSeconds(_waitTime);

        _currentWeapon.ReleaseTrigger();
        _waitTime = _currentWeapon.GetTimeUntilAnotherShot() + GetShootTimeOffset();
        //Debug.Log($"// GetTimeUntilAnotherShot() = {_waitTime}");
        yield return new WaitForSeconds(_waitTime);

        _canShoot = true;
        onStoppedShooting?.Invoke();
    }

    private float GetShootTimeOffset()
    {
        var _offset = _entitySource.GetEntitySO<AiEntitySO>().MinMaxShootTimeOffset;
        return Random.Range(_offset.x, _offset.y);
    }
}
