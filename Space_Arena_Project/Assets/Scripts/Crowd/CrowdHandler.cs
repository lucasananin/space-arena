using UnityEngine;

public class CrowdHandler : MonoBehaviour
{
    [SerializeField] Collider2D _area = null;
    [SerializeField] int _count = 100;
    [SerializeField] GameObject[] _prefabs = null;

    private void Start()
    {
        for (int i = 0; i < _count; i++)
        {
            var _prefab = _prefabs[Random.Range(0, _prefabs.Length)];
            var _position = GeneralMethods.GetRandomPointInBounds(_area.bounds);
            var _instance = Instantiate(_prefab, _position, Quaternion.identity, _area.transform);
        }
    }
}
