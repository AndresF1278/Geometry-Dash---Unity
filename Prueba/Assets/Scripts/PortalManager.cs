using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
 

    enum typesPortal 
    {
        portalBall,
        portalShip,
        portalCube,
        portalGravityNormal,
        portalGravityReverse
    }

    [SerializeField] private typesPortal typePortal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

         if(typePortal != typesPortal.portalGravityNormal && typePortal != typesPortal.portalGravityReverse)
           {
                GameObject currentVehicle = other.gameObject;
                Vector3 position = currentVehicle.transform.position;
                Vector3 velocity = currentVehicle.GetComponent<Rigidbody>().velocity;
                Destroy(currentVehicle);
               
                switch (typePortal)
                {
                    case typesPortal.portalBall:
                        currentVehicle =  Instantiate(GameManager.Instance.prefabBall, position, Quaternion.identity);
                        break;
                    case typesPortal.portalShip:
                        currentVehicle = Instantiate(GameManager.Instance.prefabShip, position, Quaternion.identity);
                        break;
                    case typesPortal.portalCube:
                        currentVehicle = Instantiate(GameManager.Instance.prefabCube, position, Quaternion.identity);
                        break;

                }
                currentVehicle.GetComponent<Rigidbody>().velocity = velocity;
                Camera.main.GetComponent<CameraFollow>().ChangeTarget(currentVehicle.transform);
                
                return;
            }



            if(typePortal == typesPortal.portalGravityNormal)
            {
                other.GetComponent<PlayerControllers>().ChangeGravityPortal(true);
            }

            if (typePortal == typesPortal.portalGravityReverse)
            {
                other.GetComponent<PlayerControllers>().ChangeGravityPortal(false);

            }

            GetComponent<BoxCollider>().enabled = false;

        }
    }

   

  
}
