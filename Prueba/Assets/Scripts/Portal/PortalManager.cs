using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum typesPortal
{
    portalBall,
    portalShip,
    portalCube,
    portalGravityNormal,
    portalGravityReverse
}
public class PortalManager : MonoBehaviour
{

    public static PortalManager instance;

    [SerializeField] private GameObject prefabParticleShip, prefabParticleCube, prefabParticleBall;

    public  typesPortal currentVehicleType;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private GameObject ParticlePurple, ParticleRed, ParticleGreen;

   public  void PortalChangeVehicle(GameObject other , typesPortal typePortal, GameObject portal)
    {
       
            GameObject currentVehicle = other.gameObject;
            Vector3 position = currentVehicle.transform.position;
            Vector3 velocity = currentVehicle.GetComponent<Rigidbody>().velocity;


                

        if(!currentVehicleType.Equals(typePortal))
        {
            Destroy(currentVehicle);
            switch (typePortal)
            {
                case typesPortal.portalBall:
                    currentVehicle = Instantiate(GameManager.Instance.prefabBall, position, Quaternion.identity);
                    currentVehicleType = typesPortal.portalBall;
                    break;
                case typesPortal.portalShip:
                    currentVehicle = Instantiate(GameManager.Instance.prefabShip, position, Quaternion.identity);
                    currentVehicleType = typesPortal.portalShip;
                    break;
                case typesPortal.portalCube:
                    currentVehicle = Instantiate(GameManager.Instance.prefabCube, position, Quaternion.identity);
                    currentVehicleType = typesPortal.portalCube;
                    break;

            }
        }

        currentVehicle.GetComponent<Rigidbody>().velocity = velocity;
        Camera.main.GetComponent<CameraFollow>().ChangeTarget(currentVehicle.transform);
        portal.GetComponent<BoxCollider>().enabled = false;

        portal.GetComponent<BoxCollider>().enabled = false;

    }

    public void PortalGravity(typesPortal typePortal, GameObject other, GameObject portal)
    {
        if (typePortal == typesPortal.portalGravityNormal)
        {
            other.GetComponent<PlayerControllers>().ChangeGravityPortal(true);
        }

        if (typePortal == typesPortal.portalGravityReverse)
        {
            other.GetComponent<PlayerControllers>().ChangeGravityPortal(false);

        }
        portal.GetComponent<BoxCollider>().enabled = false;
    }
    
    public void ActivateParticle(typesPortal type, Vector3 position)
    {

        switch (type)
        {
            case typesPortal.portalBall:
                 Instantiate(prefabParticleBall, position, Quaternion.identity);
                break;
            case typesPortal.portalShip:
                Instantiate( prefabParticleShip, position, Quaternion.identity);
                break;
            case typesPortal.portalCube:
                Instantiate(prefabParticleCube, position, Quaternion.identity);
                break;
        }

    }






}
   
    

  

