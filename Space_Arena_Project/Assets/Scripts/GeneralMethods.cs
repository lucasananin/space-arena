using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMethods : MonoBehaviour
{
    public static bool HasAvailableTag(GameObject _gameObjectHit, string[] _tags)
    {
        int _count = _tags.Length;

        for (int i = 0; i < _count; i++)
        {
            if (_gameObjectHit.CompareTag(_tags[i]))
            {
                return true;
            }
        }

        return false;
    }

    public static Vector3 RandomPointInBounds(Bounds _bounds)
    {
        return new Vector3(
            Random.Range(_bounds.min.x, _bounds.max.x),
            Random.Range(_bounds.min.y, _bounds.max.y),
            Random.Range(_bounds.min.z, _bounds.max.z)
        );
    }

    public static bool IsTheSameString(string _a, string _b)
    {
        return string.Equals(_a, _b, System.StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsPointCloseToTarget(Vector3 _point, Vector3 _target, float _minDistance)
    {
        float _distance = (_target - _point).sqrMagnitude;
        return _distance < _minDistance * _minDistance;
    }

    public static Vector2 GetRandomInCircle(float _min, float _max)
    {
        return Random.insideUnitCircle.normalized * Random.Range(_min, _max);
    }
}
