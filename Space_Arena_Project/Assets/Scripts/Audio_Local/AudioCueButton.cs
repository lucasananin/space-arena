using UnityEngine;
using UnityEngine.UI;

public class AudioCueButton : AudioCue
{
    [SerializeField] Button _button = null;

    private void OnValidate()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(PlayAudioCue);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(PlayAudioCue);
    }
}
