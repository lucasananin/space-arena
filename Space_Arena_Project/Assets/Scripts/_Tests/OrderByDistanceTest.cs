using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderByDistanceTest : MonoBehaviour
{
    //[SerializeField] List<Transform> _objects = null;
    [SerializeField] List<SpriteRenderer> _list = null;
    private Collider2D[] _results = new Collider2D[9];

    [Button]
    private void Fodase()
    {
        int _count = _list.Count;

        for (int i = 0; i < _count; i++)
        {
            var _randomPosition = new Vector2(Random.Range(-4f, 4f), Random.Range(-4f, 4f));
            _list[i].transform.position = _randomPosition;
        }

        // da um overlap para pegar os inimigos ao redor.
        var _hits = Physics2D.OverlapCircleNonAlloc(transform.position, 5, _results, LayerMask.NameToLayer("Entity"));

        // faz uma lista ordenada pelo mais proximo.
        _list = GeneralMethods.OrderListByDistance(_list, transform.position);

        // ve qual dos mais proximos esta dentro de um angulo x.
        // rotaciona em direcao a esse inimigo.
    }
}
