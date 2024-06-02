using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAnimator : EntityAnimator
{
    [SerializeField] AiEntity _aiEntity = null;

    private void LateUpdate()
    {
        bool _isMoving = _aiEntity.IsMoving();
        SetIsMoving(_isMoving);
    }
}
