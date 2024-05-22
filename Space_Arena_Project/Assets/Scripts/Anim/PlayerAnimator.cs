using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : EntityAnimator
{
    [SerializeField] PlayerMover _playerMover = null;

    private void LateUpdate()
    {
        bool _isMoving = _playerMover.HasMovementInput();
        SetIsMoving(_isMoving);
    }
}
