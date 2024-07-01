using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PUZ.Behaviour;

public class Swim : MonoBehaviour
{
    private readonly Animator _animator;
    private readonly PUZObject _object;

    public Swim( Animator animator, PUZObject puzObject)
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
