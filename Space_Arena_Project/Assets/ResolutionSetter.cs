using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSetter : MonoBehaviour
{
    //[SerializeField] Vector2 _targetAspect = new Vector2(16f, 9f);
    [SerializeField] Vector2 _referenceResolution = new Vector2(1920f, 1080f);
    [SerializeField, ReadOnly] Camera _mainCamera = null;

    private void OnValidate()
    {
        _mainCamera = GetComponent<Camera>();
    }

    private void Start()
    {
        UpdateValues();
    }

    [Button]
    private void UpdateValues()
    {
        _mainCamera.rect = new Rect(Vector2.zero, Vector2.one);
        Vector2 resTarget = new Vector2(_referenceResolution.x, _referenceResolution.y);
        Vector2 resViewport = new Vector2(Screen.width, Screen.height);
        Vector2 resNormalized = resTarget / resViewport;
        Vector2 size = resNormalized / Mathf.Max(resNormalized.x, resNormalized.y);
        _mainCamera.rect = new Rect(default, size) { center = new Vector2(0.5f, 0.5f) };

        //return;

        ////Screen.SetResolution(1440, 1080, false);
        ////float targetAspect = 16.0f / 9.0f;
        //_mainCamera.rect = new Rect(Vector2.zero, Vector2.one);
        //float targetAspect = _targetAspect.x / _targetAspect.y;
        //float windowAspect = (float)Screen.width / (float)Screen.height;
        //float scaleHeight = windowAspect / targetAspect;

        //if (scaleHeight < 1.0f)
        //{
        //    Rect rect = _mainCamera.rect;

        //    rect.width = 1.0f;
        //    rect.height = scaleHeight;
        //    rect.x = 0;
        //    rect.y = (1.0f - scaleHeight) / 2.0f;

        //    _mainCamera.rect = rect;
        //    Debug.Log($"// A");
        //}
        //else
        //{
        //    float scaleWidth = 1.0f / scaleHeight;

        //    Rect rect = _mainCamera.rect;

        //    rect.width = scaleWidth;
        //    rect.height = 1.0f;
        //    rect.x = (1.0f - scaleWidth) / 2.0f;
        //    rect.y = 0;

        //    _mainCamera.rect = rect;
        //    Debug.Log($"// B");
        //}
    }
}
