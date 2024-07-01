using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PUZ.Behaviour;

public class PlayerDetector : MonoBehaviour, IHear
{
    private bool _isCaptured;
    private int _layerMask;

    [SerializeField]
    private float _fov = 180f;

    [SerializeField]
    private PUZObject _object;

    [SerializeField]
    private float closerDistance;
    [SerializeField]
    private float distantDistance = 20f;

    public void StartPlayerDetection()
    {
        closerDistance = distantDistance /2f;
        int layerId = 8;
        _layerMask = 1 << layerId;
        StartCoroutine(SearchForPlayer());
    }

    private Transform currentPlayer;
    private float distanceFromCurrentPlayer;
    private Vector3 lastKnownPlayerPosition;

    public void UpdateCapturedState(bool flag)
    {
        _isCaptured = true;
        StopCoroutine(SearchForPlayer());
    }

    public bool BeingFollowed() => currentPlayer != null;

    public Vector3 CurrentPlayerPosition() => lastKnownPlayerPosition;

    private IEnumerator SearchForPlayer()
    {


        WaitForSeconds waitTime = new WaitForSeconds(1f);
        
        while (!_isCaptured)
        {
            yield return waitTime;
    
            Collider[] colliders = Physics.OverlapSphere(transform.position, distantDistance, _layerMask);
    
            if (colliders.Length <= 0 )
            {
                //_object.Walk(); 
                currentPlayer = null;
                yield return waitTime;
            }
    
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out CharacterController player))
                {
                    Vector3 directionToTarget = (player.transform.position - transform.position).normalized;
    
                    if (Vector3.Angle(transform.forward, directionToTarget) < _fov)
                    {
                        float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);
                        Vector3 startPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    
                        if (Physics.Raycast(startPoint, directionToTarget, distanceToTarget, _layerMask))
                        {
                            _object.Walk(); 
                            currentPlayer = null;
                            break;
                        } 

                        if(distanceToTarget <= closerDistance && !_object.IsFleeing && _object.IsAlerted)
                        {
                            _object.IsScared = true;
                        } 
             
                        else
                        {
                            if(!_object.IsFleeing) _object.IsAlerted = true;
                        } 
                        
                        currentPlayer = player.transform;
                        lastKnownPlayerPosition = currentPlayer.position;
                        break;
                    } else {

                          if(!_object.IsFleeing)
                          {
                            _object.IsWalking = true;   
                            currentPlayer = null;
                          }
                         
                         break;
                    }
                } else {
                    if(!_object.IsFleeing)
                          {
                            _object.IsWalking = true;  
                            currentPlayer = null;
                          }
                }
            }
        }
    }

    public void ResponseToSound()
    {
        if(!_object.IsAlerted) _object.IsAlerted = true;
    }

}
