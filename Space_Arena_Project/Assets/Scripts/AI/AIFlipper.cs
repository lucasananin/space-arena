using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlipper : SideFlipper
{
    [SerializeField] AIPath _aiPath = null;

    public void FlipToTarget(Vector3 _targetPosition)
    {
        FlipByCompareX(_targetPosition.x, transform.position.x);
    }

    public void FlipToMoveDirection()
    {
        FlipByCompareX(_aiPath.velocity.x, 0);
    }
}
