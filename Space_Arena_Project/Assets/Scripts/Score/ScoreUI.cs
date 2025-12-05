using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _value = null;
    [SerializeField] TextMeshProUGUI _multiplier = null;
    [SerializeField] float _punchForce = 1f;
    [SerializeField] float _duration = 1f;

    private void OnEnable()
    {
        ScoreHandler.OnValueChanged += UpdateValue;
        ScoreHandler.OnMultiplierChanged += UpdateMultiplier;
    }

    private void OnDisable()
    {
        ScoreHandler.OnValueChanged -= UpdateValue;
        ScoreHandler.OnMultiplierChanged -= UpdateMultiplier;
    }

    private void UpdateValue(int _oldValue, int _newValue)
    {
        _value.text = $"{_newValue}";
    }

    private void UpdateMultiplier(int _newValue)
    {
        _multiplier.text = _newValue < 2 ? string.Empty : $"{_newValue}x";

        _multiplier.rectTransform.DOComplete();
        _multiplier.rectTransform.DOPunchScale(Vector3.one * _punchForce, _duration);
    }
}
