using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSniperLineFx : MonoBehaviour
{
    [SerializeField] AiEntity _aiEntity = null;
    [SerializeField] AiWeaponHandler _aiWeaponHandler = null;
    [SerializeField] LineRenderer _line = null;
    [SerializeField, Range(0f, 1f)] float _minTime = 0.5f;
    [SerializeField, Range(1f, 99f)] float _lineLength = 20f;
    [SerializeField] int _modelStartIndex = 0;
    [SerializeField, ReadOnly] AiWeaponModel _aiWeaponModel = null;

    private void Awake()
    {
        _line.enabled = false;
        _line.SetPosition(1, Vector3.right * _lineLength);
        SetModelReference(_modelStartIndex);
    }

    private void LateUpdate()
    {
        _line.enabled = _aiEntity.IsTargetOnLineOfSight && HasEnoughShootTime();
    }

    public bool HasEnoughShootTime()
    {
        return _aiWeaponModel.GetNormalizedTime() > _minTime;
    }

    private void SetModelReference(int _index)
    {
        _aiWeaponModel = _aiWeaponHandler.GetModel(_index);
    }
}
