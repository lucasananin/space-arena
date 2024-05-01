using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSeeTargetTester : MonoBehaviour
{
    [SerializeField] Transform _point = null;
    [SerializeField] Transform _target = null;
    [SerializeField] TagCollectionSO _obstacleTags = null;
    [SerializeField] LayerMask _layerMask = default;

    private RaycastHit2D[] _results = new RaycastHit2D[99];

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 _mousePosition = InputHandler.GetMousePosition();
            Vector3 _worldMousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
            _worldMousePosition.z = 0f;
            _point.position = _worldMousePosition;
            bool _canSeeTargetFromPoint = CanSeeTargetFromPoint(_point.position);
            Debug.Log($"// _canSeeTargetFromPoint = {_canSeeTargetFromPoint}");
        }
    }

    public bool CanSeeTargetFromPoint(Vector3 _point)
    {
        Vector3 _vector = _target.position - _point;
        Vector3 _direction = _vector.normalized;
        float _distance = _vector.magnitude;
        int _hits = Physics2D.CircleCastNonAlloc(_point, 0.3f, _direction, _results, _distance, _layerMask);

        for (int i = 0; i < _hits; i++)
        {
            var _gameobjectHit = _results[i].collider.gameObject;
            //Debug.Log($"// _gameobjectHit[{i}] = {_gameobjectHit.name}");

            if (GeneralMethods.HasAvailableTag(_gameobjectHit, _obstacleTags.Tags)) return false;

            if (_gameobjectHit == _target.gameObject /*&& i == 0*/)
            {
                return true;
            }
        }

        return false;
    }

    [Button]
    private void EnableGraphics()
    {
        _point.gameObject.SetActive(!_point.gameObject.activeSelf);
    }
}
