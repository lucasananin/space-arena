using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCircleMinMaxTest : MonoBehaviour
{
    [SerializeField] GameObject _prefab = null;
    [SerializeField] Vector2 _minMax = default;
    [SerializeField] float _count = 99;
    [SerializeField, ReadOnly] List<GameObject> _instances = null;

    [Button]
    private void Spawn()
    {
        for (int i = 0; i < _count; i++)
        {
            var _position = GeneralMethods.GetRandomInCircle(_minMax.x, _minMax.y);
            var _instance = Instantiate(_prefab, _position, Quaternion.identity);

            _instance.gameObject.SetActive(true);
            _instances.Add(_instance);

            var _renderer = _instance.GetComponent<SpriteRenderer>();
            _renderer.color = Random.ColorHSV();
        }
    }

    [Button]
    private void Clear()
    {
        int _count = _instances.Count;

        for (int i = _count - 1; i >= 0; i--)
        {
            Destroy(_instances[i].gameObject);
        }

        _instances.Clear();
    }
}
