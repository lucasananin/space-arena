using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorRandomizer : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] _sprites = null;

    [Button]
    private void Randomize()
    {
        var _color = Random.ColorHSV();

        int _count = _sprites.Length;

        for (int i = 0; i < _count; i++)
        {
            _sprites[i].color = _color;
        }
    }
}
