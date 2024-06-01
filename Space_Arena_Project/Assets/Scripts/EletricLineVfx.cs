using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EletricLineVfx : MonoBehaviour
{
    [SerializeField] LineRenderer[] _lines = null;
    [SerializeField] float _distance = 10f;
    [SerializeField] Vector2 _minMaxY = default;
    // opcao para fazer com que as linhas tenham o y oposto.

    [Button]
    public void Fodase()
    {
        RandomizeLine(_lines[0]);
        //RandomizeLine(_lines[1]);
        CopyFirstLine();
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
