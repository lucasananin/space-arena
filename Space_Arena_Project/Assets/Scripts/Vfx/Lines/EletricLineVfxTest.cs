using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EletricLineVfxTest : MonoBehaviour
{
    [SerializeField] LineRenderer[] _lines = null;
    [SerializeField] float _distance = 10f;
    [SerializeField] Vector2 _minMaxY = default;
    [Space]
    [SerializeField] Transform _target = null;
    [SerializeField, Range(2, 9)] int _linePoints = 4;
    // modo randomize: linhas em direcoes diferentes.
    // modo tudo junto

    [Button]
    public void UpdateLines()
    {
        //RandomizeLine(_lines[0]);
        ////RandomizeLine(_lines[1]);
        //CopyFirstLine();
        UpdateLine(_lines[0]);
        UpdateLine(_lines[1]);
    }

    private void UpdateLine(LineRenderer _line)
    {
        _line.positionCount = _linePoints;

        for (int i = 0; i < _linePoints; i++)
        {
            float _t = i / ((_linePoints - 1) * 1f);
            var _position = Vector3.Lerp(transform.position, _target.position, _t);

            bool _isEven = i % 2 == 0;
            float _yDirection = _isEven ? 1 : -1;
            //_position.y += Random.Range(_minMaxY.x, _minMaxY.y) * _yDirection;

            // pega a direcao 
            var _cross = Vector3.Cross(_target.forward, _target.right);
            _position += _cross * _yDirection;

            _line.SetPosition(i, _position);
        }

        _line.SetPosition(0, transform.position);
        _line.SetPosition(_line.positionCount - 1, _target.position);
    }

    private void RandomizeLine(LineRenderer _line)
    {
        int _count = Mathf.RoundToInt(_distance);
        _line.positionCount = _count;

        for (int i = 0; i < _count; i++)
        {
            float _x = i / _distance + i;

            bool _isEven = i % 2 == 0;
            float _yDirection = _isEven ? 1 : -1;
            float _y = Random.Range(_minMaxY.x, _minMaxY.y) * _yDirection;

            Vector2 _position = new Vector2(_x, _y);
            _line.SetPosition(i, _position);
        }

        _line.SetPosition(0, transform.position);
        _line.SetPosition(_line.positionCount - 1, transform.right * _distance);
    }

    private void CopyFirstLine()
    {
        LineRenderer _firstLine = _lines[0];
        int _count = _lines.Length;

        for (int i = 1; i < _count; i++)
        {
            _lines[i].positionCount = _firstLine.positionCount;

            int _count_2 = _lines[i].positionCount;

            for (int j = 0; j < _count_2; j++)
            {
                _lines[i].SetPosition(j, _firstLine.GetPosition(j));
            }
        }
    }
}
