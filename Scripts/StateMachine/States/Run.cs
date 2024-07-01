using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PUZ.Behaviour;
using UnityEngine.AI;
using PUZ.Utilities;

public class Run : IState
{
    private readonly Animator _animator;
    private readonly PUZObject _object;

    private NavMeshAgent _agent;

    private readonly float FLEE_DISTANCE = 5f;

    public Run( Animator animator, PUZObject puzObject)
    {
         _animator = animator;
         _object = puzObject;
    }
    
    public void OnEnter()
    {
        _agent = _object.GetAgent;
        _object.Run();
        _object.GetAgent.enabled = true;
        UtilitiesManager.Instance.PlayClip("running_a", _object.transform.GetComponent<AudioSource>(), true);
    }

    public void Tick()
    {
        if (_agent.enabled)
        {
            
            var away = GetRandomPoint();
            _agent.SetDestination(away);
        }
    }

    public void OnExit()
    {
        UtilitiesManager.Instance.StopClip("running_a", _object.transform.GetComponent<AudioSource>());
    }

    private Vector3 GetRandomPoint()
    {
        var directionFromPlayer= _object.transform.position - _object.LastKnownPlayerPosition();
        directionFromPlayer.Normalize();

        var endPoint = _object.transform.position + (directionFromPlayer * FLEE_DISTANCE);


        if (NavMesh.SamplePosition(endPoint, out var hit, 10f, NavMesh.AllAreas))
        {

            return hit.position;
        }

        return _object.transform.position;
    }
}
