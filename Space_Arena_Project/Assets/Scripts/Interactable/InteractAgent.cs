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
    private List<Collider2D> _resultsList = new List<Collider2D>();

    protected virtual InteractableBehaviour SearchForInteractables()
    {
        int _hits = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _results, _layerMask);

        UpdateResultsList(_hits);

        for (int i = 0; i < _hits; i++)
        {
            var _colliderHit = _resultsList[i];

            if (_colliderHit.TryGetComponent(out InteractableBehaviour _interactableBehaviour))
            {
                return _interactableBehaviour;
            }
        }

        return null;
    }

    private void UpdateResultsList(int _hits)
    {
        _resultsList.Clear();

        for (int i = 0; i < _hits; i++)
        {
            _resultsList.Add(_results[i]);
        }

        _resultsList = GeneralMethods.OrderListByDistance(_resultsList, transform.position);
    }
}
