using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLoot : LootBehaviour
{
    public override void Init(ScriptableObject _soValue)
    {
        SendOnInit();
    }
}
