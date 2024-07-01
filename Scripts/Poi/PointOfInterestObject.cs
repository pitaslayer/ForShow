using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PUZ.Behaviour;

public class PointOfInterestObject : MonoBehaviour
{
    [SerializeField] PointOfInterest pointType;

    private void OnTriggerEnter(Collider other)
    {
        if(GetComponent<Collider>().TryGetComponent(out PUZObject puzObject))
        {
            puzObject.PlayActionPointOfInterest(pointType);
        }
    }
}
