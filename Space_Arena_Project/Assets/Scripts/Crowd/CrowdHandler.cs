using System.Collections.Generic;
using UnityEngine;

public class CrowdHandler : MonoBehaviour
{
    [SerializeField] List<PingPongFx> _crowd = null;
    [SerializeField] int _count = 0;

    private void Update()
    {
        for (int i = 0; i < _count; i++)
        {
            _crowd[i].UpdateEffect();
        }
    }

    internal void AddPingPong(PingPongFx _pingPongFx)
    {
        _crowd.Add(_pingPongFx);
        _count = _crowd.Count;
    }
}
