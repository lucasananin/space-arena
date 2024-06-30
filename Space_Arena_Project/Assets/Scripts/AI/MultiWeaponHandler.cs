using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiWeaponHandler : MonoBehaviour
{
    [SerializeField] EntityBehaviour _sourceEntity = null;
    [SerializeField] List<MultiWeaponModel> _multiWeapons = null;

    private void Awake()
    {
        int _count = _multiWeapons.Count;

        for (int i = 0; i < _count; i++)
        {
            var _m = _multiWeapons[i];
            _m.ResetTime();
            _m.InitWeapons(_sourceEntity);
        }
    }

    private void Update()
    {
        int _count = _multiWeapons.Count;

        for (int i = 0; i < _count; i++)
        {
            var _m = _multiWeapons[i];
            _m.IncreaseTime();
            // rotate
            // flip

            if (_m.HasEnoughFireTime())
            {
                _m.ResetTime();
                var _weapon = _m.GetRandomWeapon();
                StartCoroutine(Shoot_routine(_weapon));
            }
        }
    }

    //public void RotateWeapons(Vector3 _position)
    //{
    //    int _count = _multiWeapons.Count;

    //    for (int i = 0; i < _count; i++)
    //    {
    //        _multiWeapons[i].RotateWeapons(_position);
    //    }
    //}

    //public void ResetWeaponRotations()
    //{
    //    int _count = _multiWeapons.Count;

    //    for (int i = 0; i < _count; i++)
    //    {
    //        _multiWeapons[i].ResetWeaponRotations();
    //    }
    //}

    private IEnumerator Shoot_routine(WeaponBehaviour _weapon)
    {
        _weapon.PullTrigger();
        yield return null;
        _weapon.ReleaseTrigger();
        //Debug.Log($"// {_weapon.name} has been shot!");
    }

    // 2. (talvez seja melhor ignorar isso para facilitar o meu trabalho)
    // * Talvez criar um tryMultiShootActionSO? OU talvez passar os valores como parametros.
    // checa se o alvo esta dentro da distancia minima.
    // checa se o alvo esta na mira.
}

[System.Serializable]
public class MultiWeaponModel
{
    [SerializeField] List<WeaponBehaviour> _weapons = null;
    [SerializeField] Vector2 _shootRateRange = default;
    [SerializeField] bool _canRotateWhileShooting = true;
    [SerializeField] bool _canShootWhileMoving = true;
    [SerializeField] bool _onlyShootOnTargetAcquired = true;
    [SerializeField] float _shootDistance = 12f;
    //[SerializeField] bool _onlyShootOnTargetAcquired = true;
    [SerializeField, ReadOnly] bool _isShooting = false;
    //[SerializeField, ReadOnly] bool _hasTargetOnLineOfSight = false;
    [SerializeField, ReadOnly] float _shootRate = 0f;
    [SerializeField, ReadOnly] float _nextShoot = 0f;
    [SerializeField, ReadOnly] List<WeaponRotator> _rotators = null;
    [SerializeField, ReadOnly] List<WeaponFlipper> _flippers = null;

    public bool IsShooting { get => _isShooting; set => _isShooting = value; }
    public bool CanShootWhileMoving { get => _canShootWhileMoving; private set => _canShootWhileMoving = value; }
    public bool OnlyShootOnTargetAcquired { get => _onlyShootOnTargetAcquired; private set => _onlyShootOnTargetAcquired = value; }
    public float ShootDistance { get => _shootDistance; private set => _shootDistance = value; }

    public void InitWeapons(EntityBehaviour _entitySource)
    {
        int _count = _weapons.Count;

        for (int i = 0; i < _count; i++)
        {
            _weapons[i].Init(_entitySource);
        }
    }

    public void ResetTime()
    {
        _shootRate = Random.Range(_shootRateRange.x, _shootRateRange.y);
        _nextShoot = 0;
    }

    public void IncreaseTime()
    {
        _nextShoot += Time.deltaTime;
    }

    public void RotateWeapons(Vector3 _position)
    {
        if (!_canRotateWhileShooting && _isShooting) return;
        //if (_onlyShootOnTargetAcquired && !_hasTargetOnLineOfSight) return;

        int _count = _weapons.Count;

        for (int i = 0; i < _count; i++)
        {
            _rotators[i].LookAtPosition(_position);
            _flippers[i].UpdateFlip();
        }
    }

    public void ResetWeaponRotations()
    {
        int _count = _weapons.Count;

        for (int i = 0; i < _count; i++)
        {
            _rotators[i].ResetRotation();
            _flippers[i].ResetFlip();
        }
    }

    public bool HasEnoughFireTime()
    {
        return _nextShoot > _shootRate;
    }

    public WeaponBehaviour GetRandomWeapon()
    {
        int _randomIndex = Random.Range(0, _weapons.Count);
        return _weapons[_randomIndex];
    }

    [Button]
    private void SetReferences()
    {
        _rotators.Clear();
        _flippers.Clear();

        int _count = _weapons.Count;

        for (int i = 0; i < _count; i++)
        {
            _rotators.Add(_weapons[i].GetComponent<WeaponRotator>());
            _flippers.Add(_weapons[i].GetComponent<WeaponFlipper>());
        }
    }
}