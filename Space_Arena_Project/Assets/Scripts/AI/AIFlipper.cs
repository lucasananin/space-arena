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
        //GetComponent<AiWeaponHandler>().WeaponFlipper.ChangeToAimFlipData();
        FlipToX(_targetPosition.x, transform.position.x);
    }

    public void FlipToMoveDirection()
    {
        //GetComponent<AiWeaponHandler>().WeaponFlipper.ChangeToMovementFlipData();
        FlipToX(_aiPath.velocity.x, 0);
    }
}
