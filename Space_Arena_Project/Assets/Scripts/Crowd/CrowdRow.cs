using UnityEngine;

public class CrowdRow : MonoBehaviour
{
    [SerializeField] Transform _a = null;
    [SerializeField] Transform _b = null;
    [SerializeField] Color _color = Color.white;
    [SerializeField] bool _isHorizontal = true;
    [SerializeField] GameObject[] _prefabs = null;

    //[Header("// DEBUG")]
    //[SerializeField] CrowdHandler _handler = null;

    private void Start()
    {
        var _distance = GetDistance();
        int _count = (int)_distance;

        for (int i = 0; i <= _count; i++)
        {
            var _prefab = _prefabs[Random.Range(0, _prefabs.Length)];
            var _position = _a.position + GetDirection() * i;
            var _instance = Instantiate(_prefab, _position, Quaternion.identity, transform);

            var _renderer = _instance.GetComponentInChildren<SpriteRenderer>();
            _renderer.color = _color;

            //_handler.AddPingPong(_instance.GetComponent<PingPongFx>());
        }
    }

    private float GetDistance()
    {
        if (_isHorizontal)
            return Mathf.Abs(_a.position.x) + Mathf.Abs(_b.position.x);
        else
            return Mathf.Abs(_a.position.y) + Mathf.Abs(_b.position.y);
    }

    private Vector3 GetDirection()
    {
        return _isHorizontal ? Vector3.right : Vector3.down;
    }
}
