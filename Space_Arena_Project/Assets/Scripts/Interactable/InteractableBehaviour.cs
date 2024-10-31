using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InteractableBehaviour : MonoBehaviour
{
    [SerializeField] UnityEvent OnInteractedUE = null;

    public virtual void Interact(InteractAgent _agent)
    {
        OnInteractedUE.Invoke();
    }

    //public abstract void Interact(InteractAgent _agent);
    public abstract string GetText();
}
