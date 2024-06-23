using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollidableBehaviour : MonoBehaviour
{
    [SerializeField, ReadOnly] protected bool _collided = false;

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.TryGetComponent(out CollectableAgent _agent))
        {
            Collide(_agent);
        }
    }

    [Button]
    public virtual void ResetRuntimeValues()
    {
        _collided = false;
    }

    public abstract void Collide(CollectableAgent _agent);
}
