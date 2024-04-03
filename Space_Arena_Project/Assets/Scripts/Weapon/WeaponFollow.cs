using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFollow : MonoBehaviour
{
    [SerializeField] Transform _target = null;

    private void Awake()
    {
        transform.parent = null;
    }

    private void LateUpdate()
    {
        transform.position = _target.position;
    }
}
