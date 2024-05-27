using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public typesPortal typePortal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(typePortal == typesPortal.portalGravityNormal || typePortal == typesPortal.portalGravityReverse)
            {
                PortalManager.instance.PortalGravity(typePortal, other.gameObject, gameObject);
                return;
            }


            if (typePortal != PortalManager.instance.currentVehicleType)
            {
                Vector3 collisionPoint = other.ClosestPoint(transform.position);
                PortalManager.instance.ActivateParticle(typePortal, collisionPoint);
            }

            PortalManager.instance.PortalChangeVehicle(other.gameObject, typePortal, gameObject);

            
        }
    }

}
