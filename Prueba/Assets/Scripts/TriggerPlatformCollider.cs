using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlatformCollider : MonoBehaviour
{
    [SerializeField] private BoxCollider BoxCollider;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BoxCollider.enabled = true;
        }
        
    }
}
