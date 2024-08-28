using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpawner : MonoBehaviour
{
    public void SpawnAlly(AiEntity _prefab)
    {
        var _position = GeneralMethods.GetRandomInCircle(2, 4);
        Instantiate(_prefab, _position, Quaternion.identity);
    }
}
