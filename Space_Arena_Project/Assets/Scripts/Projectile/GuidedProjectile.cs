using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedProjectile : ProjectileBehaviour
{
    [Title("// Guided")]
    [SerializeField] Transform _visualTransform = null;

    private void FixedUpdate()
    {
        CheckDestroyTime();
    }

    public override void Init(ShootModel _newShootModel)
    {
        base.Init(_newShootModel);
        transform.position = GeneratePosition(_newShootModel);
        _visualTransform.localScale = Vector3.one * _projectileSO.ExplosionRadius * 2f;
        SendInitEvent();
    }

    private Vector3 GeneratePosition(ShootModel _newShootModel)
    {
        if (_newShootModel.EntitySource is AiEntity _aiEntity)
        {
            var _randomPosition = Random.insideUnitCircle * _projectileSO.MaxPositionRadius;
            return (Vector2)_aiEntity.GetTargetEntityPosition() + _randomPosition;
        }
        else
        {
            var _mouseWorldPosition = Camera.main.ScreenToWorldPoint(InputHandler.GetMousePosition());
            var _randomPosition = Random.insideUnitCircle * _projectileSO.MaxPositionRadius;
            return (Vector2)_mouseWorldPosition + _randomPosition;
        }
    }
}
