using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpriteAnimator : MonoBehaviour
{
    [SerializeField] Sprite[] _sprites = null;
    [SerializeField] SpriteRenderer _renderer = null;
    [SerializeField] float _duration = 1f;
    [SerializeField] bool _playOnStart = true;

    private void Start()
    {
        if (_playOnStart)
        {
            Play();
        }
    }

    private void Play()
    {
        StartCoroutine(Play_routine());
    }

    private IEnumerator Play_routine()
    {
        int _index = 0;
        int _count = _sprites.Length;

        for (int i = 0; i < _count; i++)
        {
            _renderer.sprite = _sprites[_index];

            float _waitTime = _duration / _sprites.Length;
            yield return new WaitForSeconds(_waitTime);

            _index++;
        }
    }
}
