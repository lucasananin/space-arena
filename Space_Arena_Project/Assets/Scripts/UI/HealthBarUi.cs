using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUi : MonoBehaviour
{
    [SerializeField] Image _fill = null;
    [SerializeField] TextMeshProUGUI _text = null;
    [SerializeField, ReadOnly] HealthBehaviour _health = null;

    private void OnDisable()
    {
        _health.onDamageTaken -= UpdateVisuals;
        _health.OnRestored -= UpdateVisuals;
        _health.onDead -= UpdateVisuals;
    }

    public void Init(HealthBehaviour _health)
    {
        this._health = _health;
        this._health.onDamageTaken += UpdateVisuals;
        this._health.OnRestored += UpdateVisuals;
        this._health.onDead += UpdateVisuals;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        var _normalizedValue = _health.GetNormalizedValue();
        _fill.fillAmount = _normalizedValue;

        var _percentageValue = Mathf.RoundToInt(_normalizedValue * 100);
        _text.text = $"{_percentageValue}%";
    }
}
