using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiWeaponHandler : MonoBehaviour
{
    [SerializeField] EntityBehaviour _entity = null;
    [SerializeField] List<MultiWeaponModel> _multiWeapons = null;

    // 1.
    // conta um tempo para atirar pelo model.
    // ao acabar o tempo, tenta atirar, se conseguir, inicia uma coroutine e ao fim dela, zera o tempo.
    // se nao conseguir atirar, zera o tempo.
    // o tempo pode ser um minMax.
    private void Awake()
    {
        int _count = _multiWeapons.Count;

        for (int i = 0; i < _count; i++)
        {
            _multiWeapons[i].ResetTime();
        }
    }

    private void Update()
    {
        int _count = _multiWeapons.Count;

        for (int i = 0; i < _count; i++)
        {
            var _m = _multiWeapons[i];
            _m.IncreaseTime();

            if (_m.CanShoot())
            {
                _m.StartShooting();
            }
        }
    }

    // 2. (talvez seja melhor ignorar isso para facilitar o meu trabalho)
    // * Talvez criar um tryMultiShootActionSO?
    // checa se o alvo esta dentro da distancia minima.
    // checa se o alvo esta na mira.
}

[System.Serializable]
public class MultiWeaponModel
{
    public string _id = null;
    [SerializeField] List<WeaponBehaviour> _weapons = null;
    [SerializeField] Vector2 _minMaxRate = default;
    [SerializeField, ReadOnly] float _fireRate = 0f;
    [SerializeField, ReadOnly] float _nextFire = 0f;
    [SerializeField, ReadOnly] List<WeaponRotator> _rotators = null;
    [SerializeField, ReadOnly] List<WeaponFlipper> _flippers = null;

    public void ResetTime()
    {
        // if !canShoot return;
        _fireRate = Random.Range(_minMaxRate.x, _minMaxRate.y);
        _nextFire = 0;
    }

    public void IncreaseTime()
    {
        _nextFire += Time.deltaTime;
    }

    public void StartShooting()
    {
        ResetTime();
        int _rand = Random.Range(0, _weapons.Count);
        Debug.Log($"// {_id} shot weapon {_rand}!");
        // StartShoot_routine();
    }

    public bool CanShoot()
    {
        return _nextFire > _fireRate;
    }
}