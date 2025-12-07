using System.Collections.Generic;
using UnityEngine;

public class CrowdRow : MonoBehaviour
{
    [SerializeField] Transform _a = null;
    [SerializeField] Transform _b = null;
    [SerializeField] Color _color = Color.white;
    [SerializeField] GameObject[] _prefabs = null;

    [Header("// DEBUG")]
    [SerializeField] List<GameObject> _spawns = null;

    private void Start()
    {
        var _distance = Mathf.Abs(_a.position.x) + Mathf.Abs(_b.position.x);
        int _count = (int)_distance;

        for (int i = 0; i <= _count; i++)
        {
            var _prefab = _prefabs[Random.Range(0, _prefabs.Length)];
            var _position = _a.position + Vector3.right * i;
            var _instance = Instantiate(_prefab, _position, Quaternion.identity, transform);

            var _renderer = _instance.GetComponent<SpriteRenderer>();
            _renderer.color = _color;

            _spawns.Add(_instance);
        }
    }
}
