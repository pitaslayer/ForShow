using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PUZ.Behaviour;
using UnityEngine.AI;
using PUZ.Utilities;

public class Idle : IState
{
    private readonly Animator _animator;
    private readonly PUZObject _object;
    
    public Idle( Animator animator, PUZObject puzObject)
    {
         _animator = animator;
         _object = puzObject;
    }

    public void OnEnter()
    {
        UtilitiesManager.Instance.PlayClip("chilling_a", _object.transform.GetComponent<AudioSource>());
    }

    public void Tick()
    {

    }

    public void OnExit()
    {
        UtilitiesManager.Instance.StopClip("chilling_a", _object.transform.GetComponent<AudioSource>());
    }
}
