using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUi : MonoBehaviour
{
    [SerializeField] Image _image_1 = null;
    [SerializeField] TextMeshProUGUI _text_1 = null;
    [SerializeField] Image _image_2 = null;
    [SerializeField] TextMeshProUGUI _text_2 = null;
    [SerializeField, ReadOnly] PlayerWeaponHandler _weaponHandler = null;
    [SerializeField, ReadOnly] AmmoHandler _ammoHandler = null;

    private void OnDisable()
    {
        _weaponHandler.OnWeaponAdded -= UpdateVisuals;
        _ammoHandler.OnAmmoChanged -= UpdateVisuals;
    }

    public void Init(PlayerWeaponHandler _weaponHandler, AmmoHandler _ammoHandler)
    {
        this._weaponHandler = _weaponHandler;
        this._ammoHandler = _ammoHandler;
        this._weaponHandler.OnWeaponAdded += UpdateVisuals;
        this._ammoHandler.OnAmmoChanged += UpdateVisuals;
        UpdateVisuals();
    }

    [Button]
    private void UpdateVisuals()
    {
        var _weapon_1 = _weaponHandler.GetFirstWeapon();
        var _weapon_2 = _weaponHandler.GetSecondWeapon();
        UpdateWeaponVisual(_weapon_1, _image_1, _text_1);
        UpdateWeaponVisual(_weapon_2, _image_2, _text_2);
    }

    private void UpdateWeaponVisual(WeaponBehaviour _weapon, Image _image, TextMeshProUGUI _text)
    {
        if (_weapon is not null)
        {
            _image.enabled = true;
            _text.enabled = true;
            _image.sprite = _weapon.WeaponSO.SpriteIcon;
            _text.text = _weapon.GetAmmoString();
        }
        else
        {
            _image.enabled = false;
            _text.enabled = false;
        }
    }
}
