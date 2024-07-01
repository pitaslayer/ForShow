using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PUZ.Behaviour;
using PUZ.Utilities;

public class Scared : IState
{
    private readonly Animator _animator;
    private readonly PUZObject _object;
    
    public Scared( Animator animator, PUZObject puzObject)
    {
         _animator = animator;
         _object = puzObject;
    }
    
    public void OnEnter()
    {
       _object.Scared();
       _object.GetAgent.enabled = false;
       UtilitiesManager.Instance.PlayClip("screaming_a", _object.transform.GetComponent<AudioSource>());
    }

    public void Tick()
    {
      
    }

    public void OnExit()
    {
        UtilitiesManager.Instance.StopClip("screaming_a", _object.transform.GetComponent<AudioSource>());
    }
}
