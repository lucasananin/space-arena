using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFollow : MonoBehaviour
{
    [SerializeField] Transform _target = null;
    [SerializeField] bool _canFollow = false;

    private void Awake()
    {
        if (!_canFollow) return;
        transform.parent = null;
    }

    private void LateUpdate()
    {
        if (!_canFollow) return;
        transform.position = _target.position;
    }
}
