
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
         
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private GameObject ParticlePurple, ParticleRed, ParticleGreen;
    private GameObject ParticlePurpleInstance, ParticleRedInstance, ParticleGreenInstance;


    private void Start()
    {
       
        ParticleRedInstance =  Instantiate(prefabParticleBall, Vector3.zero, Quaternion.identity);
        ParticlePurpleInstance =  Instantiate(prefabParticleShip, Vector3.zero, Quaternion.identity);
        ParticleGreenInstance =  Instantiate(prefabParticleCube, Vector3.zero, Quaternion.identity);

        ParticleRedInstance.SetActive(false);
        ParticleGreenInstance.SetActive(false);
        ParticlePurpleInstance.SetActive(false);


    }

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
                ParticleRedInstance.SetActive(false);
                ParticleRedInstance.transform.position = position;
                ParticleRedInstance.SetActive(true);
                break;
            case typesPortal.portalShip:
                ParticlePurpleInstance.SetActive(false);
                ParticlePurpleInstance.transform.position = position;
                ParticlePurpleInstance.SetActive(true);
                break;
            case typesPortal.portalCube:
                ParticlePurpleInstance.SetActive(false);
                ParticleGreenInstance.transform.position = position;
                ParticleGreenInstance.SetActive(true);
                break;
        }

    }
    
       
       






}
   
    

  

