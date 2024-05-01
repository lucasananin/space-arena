using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopEnemiesTester : MonoBehaviour
{
    [SerializeField] AIPath[] _enemies = null;

    private void Start()
    {
        ChangeEnemiesMoveState();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ChangeEnemiesMoveState();
        }
    }

    private void ChangeEnemiesMoveState()
    {
        int _count = _enemies.Length;

        for (int i = 0; i < _count; i++)
        {
            _enemies[i].canMove = !_enemies[i].canMove;
        }
    }
}
