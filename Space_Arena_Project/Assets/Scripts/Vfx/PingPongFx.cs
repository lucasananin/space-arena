using UnityEngine;

public class PingPongFx : MonoBehaviour
{
    [SerializeField] Transform _transform = null;
    [SerializeField] bool _useSin = true;
    [SerializeField] bool _useX = false;
    [SerializeField] bool _randomizeStart = false;
    [SerializeField, Range(0f, 9f)] float _speed = 1f;
    [SerializeField, Range(0f, 2f)] float _yMagnitude = 1f;
    [SerializeField, Range(0f, 2f)] float _xMagnitude = 1f;

    [Header("// DEBUG")]
    [SerializeField] float _time = 0;

    private void Start()
    {
        if (_randomizeStart)
        {
            _time = Random.Range(0f, 1f);
            float _y = Mathf.Sin(_time * _speed) * _yMagnitude;
            float _x = Mathf.Sin(_time * _speed) * _xMagnitude;
            Vector3 _position = new(_useX ? _x : _transform.localPosition.x, _y);
            _transform.localPosition = _position;
        }
    }

    private void Update()
    {
        float _y = _useSin ? GetSin(_yMagnitude) : GetCos(_yMagnitude);
        float _x = _useSin ? GetSin(_xMagnitude) : GetCos(_xMagnitude);
        Vector3 _position = new(_useX ? _x : _transform.localPosition.x, _y);
        _transform.localPosition = _position;

        _time += Time.deltaTime;
    }

    private float GetSin(float _magnitude)
    {
        return Mathf.Sin(_time * _speed) * _magnitude;
    }

    private float GetCos(float _magnitude)
    {
        return Mathf.Cos(_time * _speed) * _magnitude;
    }
}
