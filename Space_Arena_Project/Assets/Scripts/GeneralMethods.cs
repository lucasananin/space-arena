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
}
