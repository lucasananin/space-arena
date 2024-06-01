using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderByDistanceTest : MonoBehaviour
{
    //[SerializeField] List<Transform> _objects = null;
    //[SerializeField] Transform _mousePoint = null;
    [SerializeField] LayerMask _layerMask = default;
    [SerializeField] float _maxAngle = 45f;
    [SerializeField] GameObject _bullet = null;
    [SerializeField, ReadOnly] List<SpriteRenderer> _list = null;

    private Collider2D[] _results = new Collider2D[9];

    private void Update()
    {
        Vector3 _mousePosition = InputHandler.GetMousePosition();
        Vector3 _direction = _mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        Quaternion _rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
        transform.rotation = _rotation;

        if (Input.GetMouseButtonDown(0))
        {
            Fodase();
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            int _count = _list.Count;

            for (int i = 0; i < _count; i++)
            {
                var _randomPosition = new Vector2(Random.Range(-4f, 4f), Random.Range(-4f, 4f));
                _list[i].transform.position = _randomPosition;
            }
        }
    }

    [Button]
    private void Fodase()
    {
        // da um overlap para pegar os inimigos ao redor.
        var _hits = Physics2D.OverlapCircleNonAlloc(transform.position, 5, _results, _layerMask);

        // faz uma lista ordenada pelo mais proximo.
        _list.Clear();

        for (int i = 0; i < _hits; i++)
        {
            if (_results[i].TryGetComponent(out SpriteRenderer _spriteRenderer))
            {
                _list.Add(_spriteRenderer);
            }
        }

        _list = GeneralMethods.OrderListByDistance(_list, transform.position);

        // ve qual dos mais proximos esta dentro de um angulo x.
        int _count = _list.Count;

        for (int i = 0; i < _count; i++)
        {
            var _sprite = _list[i];
            var _angle = CalculateAngle(_sprite.transform.position);

            if (_angle < _maxAngle / 2f)
            {
                // rotaciona em direcao a esse inimigo.
                Debug.Log($"// {_sprite.name}");
                var _direction = (_sprite.transform.position - transform.position).normalized;
                float _angle2 = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                Quaternion _rotation = Quaternion.AngleAxis(_angle2, Vector3.forward);
                _bullet.transform.rotation = _rotation;
                break;
            }
        }
    }

    private int CalculateAngle(Vector3 _targetPosition)
    {
        Vector3 _targetDir = (_targetPosition - transform.position).normalized;
        float _angle = Vector3.Angle(_targetDir, transform.right);
        return (int)_angle;
    }
}
