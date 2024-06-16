using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSmokeVfx : MonoBehaviour
{
    [SerializeField] EntityBehaviour _entity = null;
    [SerializeField] ParticleSystem _ps = null;
    [SerializeField, ReadOnly] bool _isPlaying = false;

    private void LateUpdate()
    {
        if (_entity.IsMoving())
        {
            if (_isPlaying) return;

            _isPlaying = true;
            _ps.Play();
            //Debug.Log($"play");
        }
        else if (!_entity.IsMoving())
        {
            if (!_isPlaying) return;

            _isPlaying = false;
            _ps.Stop();
            //Debug.Log($"stop");
        }
    }
}
