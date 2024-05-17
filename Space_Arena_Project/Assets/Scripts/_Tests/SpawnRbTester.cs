using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRbTester : MonoBehaviour
{
    [SerializeField] Transform _center = null;
    [SerializeField] Rigidbody2D _rbPrefab = null;
    [SerializeField] int _spawnCount = 5;
    [SerializeField] Vector2 _minMaxForce = default;

    [Button]
    private void Spawn()
    {
        for (int i = 0; i < _spawnCount; i++)
        {
            var _centerPosition = (Vector2)_center.position;
            var _randomRotation = Random.rotation;
            _randomRotation.eulerAngles = new Vector3(0f, 0f, _randomRotation.eulerAngles.z);

            var _instance = Instantiate(_rbPrefab, _centerPosition, _randomRotation);

            var _randomPosition = Random.insideUnitCircle + _centerPosition;
            var _direction = (_randomPosition - _centerPosition).normalized;
            var _force = _direction * Random.Range(_minMaxForce.x, _minMaxForce.y);
            _instance.AddForce(_force, ForceMode2D.Impulse);
        }
    }
}
