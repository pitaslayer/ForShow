using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PUZ.Behaviour;
using PUZ.Utilities;

public class Alert : IState
{
    private readonly Animator _animator;
    private readonly PUZObject _object;
    
    public Alert( Animator animator, PUZObject puzObject)
    {
         _animator = animator;
         _object = puzObject;
    }
    
    public void OnEnter()
    {
        _object.Alerted();
        _object.GetAgent.enabled = false;
        UtilitiesManager.Instance.PlayClip("notice_a", _object.transform.GetComponent<AudioSource>());
    }

    public void Tick()
    {
        
    }

    public void OnExit()
    {
        UtilitiesManager.Instance.StopClip("notice_a", _object.transform.GetComponent<AudioSource>());
    }
}
