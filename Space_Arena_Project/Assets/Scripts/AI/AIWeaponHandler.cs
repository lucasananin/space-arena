using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiWeaponHandler : MonoBehaviour
{
    [SerializeField] EntityBehaviour _entitySource = null;
    [SerializeField] List<AiWeaponModel> _weaponModels = null;

    public event System.Action OnShoot = null;

    private void OnValidate()
    {
        UpdateReferences();
    }

    private void Awake()
    {
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

    public void TryShootAll(AiEntity _aiEntity)
    {
        int _count = _weaponModels.Count;

        for (int i = 0; i < _count; i++)
        {
            var _model = _weaponModels[i];

            if (_model.ResetTimeOnLostTarget && !_aiEntity.IsTargetOnLineOfSight)
            {
                _model.ResetTime();
                continue;
            }

            _model.IncreaseTime();

            if (_model.IsShooting) continue;
            if (!_model.IsShootable()) continue;
            if (!_model.HasEnoughFireTime()) continue;
            if (!_model.CanShootWhileMoving && _aiEntity.IsMoving()) continue;
            if (!_aiEntity.IsCloseToTargetEntity(_model.ShootDistance)) continue;
            if (_model.OnlyShootOnTargetAcquired && !_aiEntity.IsTargetOnLineOfSight) continue;

            _model.ResetTime();
            StartCoroutine(Shoot_routine(_model));
        }
    }

    private IEnumerator Shoot_routine(AiWeaponModel _model)
    {
        _model.IsShooting = true;
        yield return null;

        var _weapon = _model.GetRandomWeapon();
        _weapon.PullTrigger();
        float _waitTime = _weapon.GetPullTriggerTotalTime();
        yield return new WaitForSeconds(_waitTime);

        OnShoot?.Invoke();

        _weapon.ReleaseTrigger();
        _waitTime = _weapon.GetTimeUntilAnotherShot();
        yield return new WaitForSeconds(_waitTime);

        _model.IsShooting = false;
    }

    public bool IsBlockingRotation()
    {
        int _count = _weaponModels.Count;

        for (int i = 0; i < _count; i++)
        {
            if (_weaponModels[i].CanBlockRotation())
                return true;
        }

        return false;
    }

    private void UpdateReferences()
    {
        int _count = _weaponModels.Count;
        for (int i = 0; i < _count; i++)
            _weaponModels[i].SetReferences();
    }

    public AiWeaponModel GetModel(int _index)
    {
        return _weaponModels[_index];
    }
}
