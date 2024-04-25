using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBehaviour : MonoBehaviour
{
    [SerializeField] HealthBehaviour _healthBehaviour = null;

    public bool IsAlive()
    {
        return _healthBehaviour.IsAlive();
    }
}
