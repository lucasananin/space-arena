using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] Transform _target = null;
    [SerializeField] Vector3 _offset = default;

    private void LateUpdate()
    {
        transform.position = _target.position + _offset;
    }
}
