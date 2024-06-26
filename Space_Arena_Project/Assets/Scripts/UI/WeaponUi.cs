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

    public void Init(PlayerWeaponHandler _weaponHandler)
    {
        this._weaponHandler = _weaponHandler;
        UpdateVisuals();
    }

    [Button]
    private void UpdateVisuals()
    {
        var _weapon_1 = _weaponHandler.GetFirstWeapon();
        var _weapon_2 = _weaponHandler.GetSecondWeapon();
        UpdateWeaponVisual(_weapon_1, _image_1, _text_1);
        UpdateWeaponVisual(_weapon_2, _image_2, _text_2);

        //var _weapon_1 = _weaponHandler.CurrentWeapon;

        //if (_weapon_1 is not null)
        //{
        //    _image_1.enabled = true;
        //    _text_1.enabled = true;

        //    _image_1.sprite = _weapon_1.WeaponSO.SpriteIcon;
        //    _text_1.text = _weapon_1.GetAmmoString();
        //}
        //else
        //{
        //    _image_1.enabled = false;
        //    _text_1.enabled = false;
        //}

        //var _weapon_2 = _weaponHandler.LastWeapon;

        //if (_weapon_2 is not null)
        //{
        //    _image_2.sprite = _weapon_2.WeaponSO.SpriteIcon;
        //    _text_2.text = _weapon_2.GetAmmoString();
        //}
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
