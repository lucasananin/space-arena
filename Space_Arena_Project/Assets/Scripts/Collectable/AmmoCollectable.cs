using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollectable : CollectableBehaviour
{
    public override void OnCollect()
    {
        Debug.Log($"// Collected ammo!");
    }
}
