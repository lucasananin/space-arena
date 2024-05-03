using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractAgent : MonoBehaviour
{
    [SerializeField] protected LayerMask _layerMask = default;
    [SerializeField] protected float _radius = 1f;

    [Title("// Debug")]
    [SerializeField, ReadOnly] protected InteractableBehaviour _currentInteractable = null;

    private Collider2D[] _results = new Collider2D[9];

    protected virtual InteractableBehaviour SearchForInteractables()
    {
        int _hits = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _results, _layerMask);

        for (int i = 0; i < _hits; i++)
        {
            Collider2D _colliderHit = _results[i];

            // Ver qual está mais próximo.

            if (_colliderHit.TryGetComponent(out InteractableBehaviour _interactableBehaviour))
            {
                return _interactableBehaviour;
            }
        }

        return null;
    }
}
