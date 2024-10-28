using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMusicPlayer : MonoBehaviour
{
    [SerializeField] AudioCueEventChannelSO _eventChannelSO = null;
    [SerializeField] AudioConfigurationSO _configurationSO = null;
    [SerializeField] AudioCueSO _audioCueSO = null;
    [SerializeField] bool _playOnStart = false;

    private void Start()
    {
        if (_playOnStart)
        {
            Play();
        }
    }

    public virtual void Play()
    {
        _eventChannelSO.RaisePlayEvent(_audioCueSO, _configurationSO);
    }
}
