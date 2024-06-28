using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiWeaponHandler : MonoBehaviour
{
    [SerializeField] EntityBehaviour _entity = null;
    [SerializeField] List<MultiWeaponModel> _multiWeapons = null;

    private void Awake()
    {
        int _count = _multiWeapons.Count;

        for (int i = 0; i < _count; i++)
        {
            var _m = _multiWeapons[i];
            _m.ResetTime();
            _m.InitWeapons(_entity);
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
    [SerializeField] Vector2 _minMaxRate = default;
    [SerializeField, ReadOnly] float _fireRate = 0f;
    [SerializeField, ReadOnly] float _nextFire = 0f;
    [SerializeField, ReadOnly] List<WeaponRotator> _rotators = null;
    [SerializeField, ReadOnly] List<WeaponFlipper> _flippers = null;

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
        _fireRate = Random.Range(_minMaxRate.x, _minMaxRate.y);
        _nextFire = 0;
    }

    public void IncreaseTime()
    {
        _nextFire += Time.deltaTime;
    }

    public WeaponBehaviour GetRandomWeapon()
    {
        int _randomIndex = Random.Range(0, _weapons.Count);
        return _weapons[_randomIndex];
    }

    public bool HasEnoughFireTime()
    {
        return _nextFire > _fireRate;
    }

    [Button]
    private void SetReferences()
    {
        int _count = _weapons.Count;
        _rotators.Clear();
        _flippers.Clear();

        for (int i = 0; i < _count; i++)
        {
            _rotators.Add(_weapons[i].GetComponent<WeaponRotator>());
            _flippers.Add(_weapons[i].GetComponent<WeaponFlipper>());
        }
    }
}