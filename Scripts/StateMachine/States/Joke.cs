using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PUZ.Behaviour;

public class Joke : IState
{
    private readonly Animator _animator;
    private readonly PUZObject _object;
    
    public Joke( Animator animator, PUZObject puzObject)
    {
         _animator = animator;
         _object = puzObject;
    }

    public void OnEnter()
    {
       
    }

    public void Tick()
    {
        
    }

    public void OnExit()
    {
        
    }
}
