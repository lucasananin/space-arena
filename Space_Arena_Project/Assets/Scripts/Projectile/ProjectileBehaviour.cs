using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] protected LayerMask _layerMask = default;
    [SerializeField, ReadOnly] protected ShootModel _shootModel = null;

    public abstract void Init(ShootModel _shootModel);

    public bool HasHitSource(GameObject _gameobjectHit)
    {
        return _gameobjectHit == _shootModel.CharacterSource;
    }
}
