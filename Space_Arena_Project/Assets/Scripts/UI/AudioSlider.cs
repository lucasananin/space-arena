using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] Slider _slider = null;
    [SerializeField] FloatEventChannelSO _masterVolumeEventChannel = default;
    [SerializeField, ReadOnly] AudioManager _audioManager = null;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private IEnumerator Start()
    {
        yield return null;
        _slider.value = _audioManager.GetMasterVolumeNormalized();
        _slider.onValueChanged.AddListener(_masterVolumeEventChannel.RaiseEvent);
    }

    //private void OnEnable()
    //{
    //    _slider.onValueChanged.AddListener(_masterVolumeEventChannel.RaiseEvent);
    //}

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveAllListeners();
    }
}
