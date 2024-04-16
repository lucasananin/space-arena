using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SimpleExplosionVfx : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer = null;
    [SerializeField] float _duration = 1f;

    public void Init(float _radius)
    {
        transform.localScale = Vector3.one * _radius * 2f;
        _spriteRenderer.DOFade(0f, _duration);
        StartCoroutine(DestroyRoutine());
    }

    private IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(_duration * Random.Range(1.5f, 2f));

        Destroy(gameObject);
    }
}
