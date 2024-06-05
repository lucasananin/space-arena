using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarFx : MonoBehaviour
{
    [SerializeField] ProjectileBehaviour _projectileBehaviour = null;
    [SerializeField] Transform _prefab = null;
    [SerializeField] Ease _ease = default;
    [SerializeField] float _jumpPower = 1f;
    [SerializeField] int _numJumps = 1;

    private void OnEnable()
    {
        _projectileBehaviour.OnInit += Play;
    }

    private void OnDisable()
    {
        _projectileBehaviour.OnInit -= Play;
    }

    private void Play(ShootModel _shootModel)
    {
        var _position = _shootModel.WeaponSource.Muzzle.position;
        var _instance = Instantiate(_prefab, _position, Quaternion.identity);
        _instance.gameObject.SetActive(true);

        var _duration = _projectileBehaviour.TimeUntilDestroy;
        var _jumpPower = this._jumpPower * _duration;
        _instance.DOJump(transform.position, _jumpPower, _numJumps, _duration).
            SetEase(_ease).
            OnComplete(() =>
            {
                Destroy(_instance.gameObject);
            });
    }
}
