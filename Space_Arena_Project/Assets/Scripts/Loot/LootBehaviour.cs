using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LootBehaviour : MonoBehaviour
{
    public event System.Action onInit = null;

    public abstract void Init(ScriptableObject _soValue);

    protected virtual void SendOnInit()
    {
        onInit?.Invoke();
    }
}
