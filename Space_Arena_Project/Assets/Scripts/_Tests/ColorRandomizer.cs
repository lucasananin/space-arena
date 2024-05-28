using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer = null;

    private void OnValidate()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    [Button]
    private void RandomizeColor()
    {
        _spriteRenderer.color = Random.ColorHSV();
    }
}
