using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLineVfx : CastLineVfx
{
    public void Init(Vector3 _newPosition)
    {
        int _count = _lineRenderers.Length;

        for (int i = 0; i < _count; i++)
        {
            _lineRenderers[i].SetPosition(0, transform.position);
            _lineRenderers[i].SetPosition(1, _newPosition);
        }

        ResetShrinkTimer();
        StartDestroyRoutine();
    }
}
