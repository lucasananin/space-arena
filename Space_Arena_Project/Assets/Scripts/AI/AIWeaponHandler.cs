using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiWeaponHandler : MonoBehaviour
{
    [SerializeField] EntityBehaviour _entitySource = null;
    [SerializeField] WeaponBehaviour _currentWeapon = null;
    [SerializeField] List<MultiWeaponModel> _weaponModels = null;

    [Title("// Debug")]
    [SerializeField, ReadOnly] bool _isShooting = false;

    public event System.Action OnShoot = null;

    public bool IsShooting { get => _isShooting; private set => _isShooting = value; }

    private void Awake()
    {
        _currentWeapon.Init(_entitySource);

        int _count = _weaponModels.Count;

        for (int i = 0; i < _count; i++)
        {
            var _m = _weaponModels[i];
            _m.ResetTime();
            _m.InitWeapons(_entitySource);
        }
    }

    public void RotateWeapons(Vector3 _position)
    {
        int _count = _weaponModels.Count;
        for (int i = 0; i < _count; i++)
            _weaponModels[i].RotateWeapons(_position);
    }

    public void ResetWeaponRotations()
    {
        int _count = _weaponModels.Count;
        for (int i = 0; i < _count; i++)
            _weaponModels[i].ResetWeaponRotations();
    }

    public void IncreaseTimer()
    {
        _weaponModels[0].IncreaseTime();
    }

    public void StartShooting()
    {
        if (_isShooting) return;

        StartCoroutine(StartShooting_routine(_weaponModels[0]));
        //StartCoroutine(StartShooting_routine(_currentWeapon));
    }

    // Se o inimigo possuir um "action" que permita a troca de armas, 
    // vai ser preciso fazer ela esperar o _canShoot
    // ou dar um stopCoroutine e resetar o _canShoot ao fazer a troca.
    private IEnumerator StartShooting_routine(MultiWeaponModel _model)
    {
        _model.IsShooting = true;
        _isShooting = true;
        yield return null;

        var _weapon = _model.GetRandomWeapon();
        _weapon.PullTrigger();
        float _waitTime = _weapon.GetPullTriggerTotalTime();
        //Debug.Log($"// GetPullTriggerTotalTime = {_waitTime}");
        yield return new WaitForSeconds(_waitTime);

        OnShoot?.Invoke();

        _weapon.ReleaseTrigger();
        _waitTime = _weapon.GetTimeUntilAnotherShot() + GetShootTimeOffset();
        //Debug.Log($"// GetTimeUntilAnotherShot() = {_waitTime}");
        yield return new WaitForSeconds(_waitTime);

        _model.IsShooting = false;
        _isShooting = false;
    }

    private float GetShootTimeOffset()
    {
        // botar o shootTime aqui ao inves de ficar aumentando o tempo todo?
        // ** nao vai funcionar com o multiweapons, pois ele vai disparar todas as armas ao iniciar.
        var _offset = _entitySource.GetEntitySO<AiEntitySO>().MinMaxShootTimeOffset;
        return Random.Range(_offset.x, _offset.y);
    }
}
