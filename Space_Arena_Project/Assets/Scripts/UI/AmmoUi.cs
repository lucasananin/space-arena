using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUi : MonoBehaviour
{
    [SerializeField] Image[] _fills = null;
    [SerializeField, ReadOnly] AmmoHandler _ammoHandler = null;

    private void OnDisable()
    {
        _ammoHandler.OnAmmoChanged -= UpdateVisuals;
    }

    public void Init(AmmoHandler _ammoHandler)
    {
        this._ammoHandler = _ammoHandler;
        this._ammoHandler.OnAmmoChanged += UpdateVisuals;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        int _count = _ammoHandler.Models.Length;

        for (int i = 0; i < _count; i++)
        {
            var _model = _ammoHandler.Models[i];
            _fills[i].fillAmount = _model.GetNormalizedValue();
        }
    }

    [Button]
    private void RandomizeFills()
    {
        int _count = _fills.Length;

        for (int i = 0; i < _count; i++)
        {
            _fills[i].fillAmount = Random.value;
            _fills[i].color = Random.ColorHSV();
        }
    }
}
