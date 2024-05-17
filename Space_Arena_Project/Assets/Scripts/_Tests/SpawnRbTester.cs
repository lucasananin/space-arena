using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRbTester : MonoBehaviour
{
    [SerializeField] Transform _center = null;
    [SerializeField] Rigidbody2D _rbPrefab = null;
    [SerializeField] int _spawnCount = 5;

    [Button]
    private void Spawn()
    {
        for (int i = 0; i < _spawnCount; i++)
        {
            var _centerPosition = (Vector2)_center.position;
            var _instance = Instantiate(_rbPrefab, _center.position, Quaternion.identity);

            var _randomPosition = Random.insideUnitCircle.normalized * Random.Range(1f, 3f);
            _randomPosition += _centerPosition;

            var _direction = (_randomPosition - _centerPosition).normalized;
            var _force = _direction * Random.Range(2f, 4f);
            _instance.AddForce(_force, ForceMode2D.Impulse);
        }
    }
}
