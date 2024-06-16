using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : EntityBehaviour
{
    [SerializeField] PlayerMover _mover = null;

    private void OnValidate()
    {
        if (_mover is null)
            _mover = GetComponent<PlayerMover>();
    }

    public override bool IsMoving()
    {
        return _mover.HasMovementInput();
    }
}
