using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnVfx : MonoBehaviour
{
    [SerializeField] GameObject _player = null;
    [SerializeField] ParticleSystem _vfx = null;

    public void Play()
    {
        StartCoroutine(Play_routine());
    }

    private IEnumerator Play_routine()
    {
        yield return null;
        _vfx.transform.parent = null;
        _vfx.Play();
        _player.gameObject.SetActive(false);
    }
}
