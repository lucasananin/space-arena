using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_AISearchTarget", menuName = "SO/State Machines/Actions/AI Search Target")]
public class SearchTargetActionSO : StateActionSO<SearchTargetAction>
{
    [SerializeField] LayerMask _layerMask = default;
    [SerializeField] float _radius = 5f;
    [SerializeField] string[] _tags = null;

    public LayerMask LayerMask { get => _layerMask; private set => _layerMask = value; }
    public float Radius { get => _radius; private set => _radius = value; }
    public string[] Tags { get => _tags; private set => _tags = value; }
}

public class SearchTargetAction : StateAction
{
    private new SearchTargetActionSO OriginSO => (SearchTargetActionSO)base.OriginSO;

    private StateMachine _stateMachine = null;
    private AIEntity _aIEntity = null;
    //private AIWeaponHandler _aIWeaponHandler = null;
    private Collider2D[] _results = new Collider2D[9];

    public override void Awake(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _aIEntity = _stateMachine.GetComponent<AIEntity>();
        //_aIWeaponHandler = _stateMachine.GetComponent<AIWeaponHandler>();
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        //if (_aIWeaponHandler.HasTargetEntity()) return;
        if (_aIEntity.HasTargetEntity()) return;

        int _hits = Physics2D.OverlapCircleNonAlloc(_stateMachine.transform.position, OriginSO.Radius, _results, OriginSO.LayerMask);

        for (int i = 0; i < _hits; i++)
        {
            Collider2D _colliderHit = _results[i];

            if (HitSource(_colliderHit.gameObject)) continue;

            // Colocar as tags em um SO e criar uma lista de SO para saber quem atacar.
            if (HasAvailableTag(_colliderHit))
            //if (_colliderHit.CompareTag("Player"))
            {
                _aIEntity.SetTargetEntity(_colliderHit.gameObject);
                //_aIWeaponHandler.SetTargetEntity(_colliderHit.gameObject);
                //Debug.Log($"// Found a Player target!", _stateMachine);
            }
        }
    }

    private bool HitSource(GameObject _gameObjectHit)
    {
        return _stateMachine.gameObject == _gameObjectHit;
    }

    private bool HasAvailableTag(Collider2D _colliderHit)
    {
        int _count = OriginSO.Tags.Length;

        for (int i = 0; i < _count; i++)
        {
            if (_colliderHit.CompareTag(OriginSO.Tags[i]))
            {
                return true;
            }
        }

        return false;
    }
}