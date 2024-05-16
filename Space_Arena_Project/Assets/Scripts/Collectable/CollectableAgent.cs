using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableAgent : MonoBehaviour
{
    //[SerializeField] protected LayerMask _layerMask = default;
    //[SerializeField] protected float _radius = 1f;

    //private Collider2D[] _results = new Collider2D[9];

    //private void Update()
    //{
    //    SearchForCollectables();
    //}

    //protected virtual void SearchForCollectables()
    //{
    //    int _hits = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _results, _layerMask);

    //    for (int i = 0; i < _hits; i++)
    //    {
    //        Collider2D _colliderHit = _results[i];

    //        if (_colliderHit.TryGetComponent(out CollectableBehaviour _collectableBehaviour))
    //        {
    //            _collectableBehaviour.SetAgent(this);
    //        }
    //    }
    //}
}
