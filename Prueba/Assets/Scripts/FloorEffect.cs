using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorEffect : Orbs
{
    private TypeOrbs type;
    private Rigidbody rb;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
         {
            rb = other.GetComponent<Rigidbody>();
            switch (typeOrb)
            {
                case TypeOrbs.purple:
                    EffectPurple(rb);
                    break;
                case TypeOrbs.yellow:
                    EffectYellow(rb);
                    break;
                case TypeOrbs.blue:
                    EffectBlue(rb);
                    rb.velocity = Vector3.zero;
                    break;
                default:
                    break;
            }
            GetComponent<BoxCollider>().enabled = false;
        }
        
    }


}
