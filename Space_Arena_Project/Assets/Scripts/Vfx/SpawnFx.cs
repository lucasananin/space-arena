//using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFx : MonoBehaviour
{
    //[SerializeField] Transform _transform = null;
    [SerializeField] ParticleSystem _ps = null;
    //[SerializeField] float _duration = 0.2f;
    [SerializeField] float _waitTime = 1f;
    [SerializeField, ReadOnly] bool _isAnimating = false;
    //[Space]
    //[SerializeField] Transform _circle = null;
    //[SerializeField] ParticleSystem _ps = null;

    public bool IsAnimating { get => _isAnimating; private set => _isAnimating = value; }

    public void Play()
    {
        StartCoroutine(Play_routine());
        //_isAnimating = true;
        //_transform.localScale = Vector3.zero;
        //_ps.Play();

        //_transform.DOScale(Vector3.one, _duration).
        //    OnComplete(() =>
        //    {
        //        StartCoroutine(Play_routine());
        //    });
    }

    private IEnumerator Play_routine()
    {
        _ps.Play();
        _isAnimating = true;
        yield return new WaitForSeconds(_waitTime);
        _isAnimating = false;
    }

    //[Button]
    //public void PlayNew()
    //{
    //    _isAnimating = true;

    //    _transform.localScale = Vector3.zero;
    //    _circle.gameObject.SetActive(true);
    //    _circle.position += Vector3.up * 10f;

    //    _circle.DOMove(transform.position, 0.2f).
    //        OnComplete(() =>
    //        {
    //            _circle.gameObject.SetActive(false);
    //            _ps.Play();
    //            Play();
    //        });
    //}
}
