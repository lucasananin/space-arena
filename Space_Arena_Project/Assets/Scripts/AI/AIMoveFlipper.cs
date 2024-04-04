using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveFlipper : SideFlipper
{
    [SerializeField] AIPath _aiPath = null;
    [SerializeField, ReadOnly] bool _isMovingRight = true;

    private void Update()
    {
        FlipToMoveDirection();
    }

    private void FlipToMoveDirection()
    {
        if (_aiPath.velocity.x > 0)
        {
            _isMovingRight = true;
        }
        else if (_aiPath.velocity.x < 0)
        {
            _isMovingRight = false;
        }

        Flip(_isMovingRight);
    }
}
