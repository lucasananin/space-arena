using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlipper : SideFlipper
{
    [SerializeField] AIPath _aiPath = null;

    public void FlipToMoveDirection()
    {
        FlipToX(_aiPath.velocity.x, 0);
    }
}
