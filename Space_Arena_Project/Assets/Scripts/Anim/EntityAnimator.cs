using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityAnimator : MonoBehaviour
{
    [SerializeField] Animator _animator = null;

    private int _isMoving_hash = Animator.StringToHash("IsMoving");

    public void SetIsMoving(bool _isMoving)
    {
        _animator.SetBool(_isMoving_hash, _isMoving);
    }
}
