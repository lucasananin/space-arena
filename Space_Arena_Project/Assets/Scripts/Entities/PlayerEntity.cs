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

    //private void OnEnable()
    //{
    //    EnemySpawner.OnEndWaveGroupChanged += ResetPosition;
    //}

    //private void OnDisable()
    //{
    //    EnemySpawner.OnEndWaveGroupChanged -= ResetPosition;
    //}

    public override bool IsMoving()
    {
        return _mover.HasMovementInput();
    }

    private void ResetPosition(WaveSO _waveSo)
    {
        transform.position = Vector3.zero;
    }
}
