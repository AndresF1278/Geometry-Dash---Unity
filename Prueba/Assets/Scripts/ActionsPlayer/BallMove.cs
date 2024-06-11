using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool Ground;
    [SerializeField] private float timer;
    [SerializeField] private bool timeBool;
    [SerializeField] private float timeBuffer = 0.2f;
    [SerializeField] private GameObject body;
    [SerializeField] private PlayerControllers playerController;

    private void Start()
    {
        PortalManager.instance.currentVehicleType = typesPortal.portalBall;
        playerController = GetComponent<PlayerControllers>();
        timer = 0;
        timeBool = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && !Ground)
        {
            timeBool = true;
            timer = 0;
        }

        if (timeBool)
        {
            timer += Time.deltaTime;
            
            if(timer < timeBuffer && Ground)
            {
                GravityChange();
                timer = 0;
                timeBool= false;
            }
        }


        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) )
        {
            if( Ground && NearGround() && !playerController.DetectOrb())
            {
                
                GravityChange();
            }
         

        }

        


        body.transform.Rotate(360 * speed * Time.deltaTime, 0, 0);

    }




    private void GravityChange()
    {
        GetComponent<PlayerControllers>().ChangeGravityPlayer();
       //Physics.gravity *= -1;
       // GetComponent<PlayerControllers>().reverseGravity = !GetComponent<PlayerControllers>().reverseGravity;

        if (Physics.gravity.y < 0)
        {
            body.transform.rotation = Quaternion.Euler(body.transform.rotation.x, 0, 0);

        }
        else
        {
            body.transform.rotation = Quaternion.Euler(body.transform.rotation.x, 0 , 180);

        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Ground = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Ground = false;
        }
    }
    private bool NearGround()
    {
        RaycastHit hit;

        // Lanzar un rayo hacia abajo desde la posición actual del objeto
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f) && Physics.gravity.y < 0)
        {
            // Comprobar si el objeto golpeado está en la capa deseada
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Floor"))
            {
                // Retornar verdadero si el objeto golpeado está en la capa deseada
                return true;
            }
        }
        if (Physics.Raycast(transform.position, Vector3.up, out hit,1f) && Physics.gravity.y > 0)
        {
            // Comprobar si el objeto golpeado está en la capa deseada
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Floor"))
            {
                // Retornar verdadero si el objeto golpeado está en la capa deseada
                return true;
            }
        }




        // Si no golpea nada o no está en la capa deseada, retornar falso
        return false;

    }


    private void OnDrawGizmos()
    {
        // Dibuja el rayo en el editor
        Gizmos.color = Color.red;
        if (Physics.gravity.y < 0)
        {
            Gizmos.DrawRay(transform.position, Vector3.down * 1f);
        }
        else
        {
            Gizmos.DrawRay(transform.position, Vector3.up * 1f);
        }

        
        
    }
}
