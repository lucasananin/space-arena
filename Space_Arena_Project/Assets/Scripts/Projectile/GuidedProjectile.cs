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

        var _mouseWorldPosition = Camera.main.ScreenToWorldPoint(InputHandler.GetMousePosition());
        var _randomPosition = Random.insideUnitCircle * _projectileSO.MaxPositionRadius;
        var _finalPosition = (Vector2)_mouseWorldPosition + _randomPosition;
        transform.position = _finalPosition;

        _visualTransform.localScale = Vector3.one * _projectileSO.ExplosionRadius * 2f;

        SendInitEvent();
    }
}
