using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] float rangeToPropagate = 4f;
    public void MakeSound() 
    {
        int layerMask = 1 << 15; // 15 = Agent's Layer Mask (filter, for faster and more optimal search)
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, rangeToPropagate, layerMask);

        foreach (Collider collider in colliders) 
        {
            if (collider.TryGetComponent(out IHear hearer))
            {
                hearer.ResponseToSound();
            }
        }
    }
}
