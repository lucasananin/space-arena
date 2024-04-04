using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiFollowTest : AIPath
{
    //[SerializeField] Seeker _seeker = null;
    //[SerializeField] AIPath _aiPath = null;
    //[SerializeField] AIDestinationSetter _aIDestinationSetter = null;

    public override void OnTargetReached()
    {
        base.OnTargetReached();
        Debug.Log($"// TargetReached");
    }
}
