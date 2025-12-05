using UnityEngine;
using UnityEngine.Events;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] int _value = 0;
    [SerializeField] int _multiplier = 0;
    [SerializeField] float _timer = 0f;
    [SerializeField] float _duration = 1f;

    public static event UnityAction<int, int> OnValueChanged = null;
    public static event UnityAction<int> OnMultiplierChanged = null;

    private void OnEnable()
    {
        EnemyHealth.OnAnyAiDead += IncreaseValue;
    }

    private void OnDisable()
    {
        EnemyHealth.OnAnyAiDead -= IncreaseValue;
    }

    private void Start()
    {
        OnValueChanged?.Invoke(0, _value);
        OnMultiplierChanged?.Invoke(_multiplier);
    }

    private void LateUpdate()
    {
        if (_timer > _duration) return;

        _timer += Time.deltaTime;

        if (_timer > _duration)
        {
            _timer = 0;
            _multiplier = 0;
            OnMultiplierChanged?.Invoke(_multiplier);
        }
    }

    private void IncreaseValue(HealthBehaviour obj)
    {
        _timer = 0;
        _multiplier++;
        OnMultiplierChanged?.Invoke(_multiplier);

        var _temp = _value;
        _value += 10 * _multiplier;
        OnValueChanged?.Invoke(_temp, _value);
    }
}
