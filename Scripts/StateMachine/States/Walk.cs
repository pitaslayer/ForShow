using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PUZ.Behaviour;
using UnityEngine.AI;
using PUZ.Utilities;

public class Walk : IState
{
    private readonly Animator _animator;
    private readonly PUZObject _object;

    private NavMeshAgent _agent;
    private float timer;
    
    private float wanderRadius = 10f;
    private float wanderTimer = 5f;
    
    public Walk( Animator animator, PUZObject puzObject)
    {
         _animator = animator;
         _object = puzObject;
    }

    public void OnEnter()
    {
       _agent = _object.GetAgent;
       _object.IsWalking = true;
       UtilitiesManager.Instance.PlayClip("walking_a", _object.transform.GetComponent<AudioSource>(), true);
    }

    public void Tick()
    {
        timer += Time.deltaTime;
 
        if (timer >= wanderTimer) {

            Vector3 newPos =_object.RandomNavSphere(_object.transform.position, wanderRadius, -1);
            _agent.SetDestination(newPos);
            timer = 0;
        }
    }

    public void OnExit()
    {
        _object.IsWalking = false;
        UtilitiesManager.Instance.StopClip("walking_a", _object.transform.GetComponent<AudioSource>());
    }
}
