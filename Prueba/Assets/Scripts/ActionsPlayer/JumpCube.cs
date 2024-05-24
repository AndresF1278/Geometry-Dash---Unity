using UnityEngine;
using DG.Tweening;
using System;
public class JumpCube : MonoBehaviour
{
    public float jumpForce = 10f; // Fuerza del salto
    public float rotationSpeed = 100f; // Velocidad de rotación del cubo
    public float Speed = 100f; // Velocidad de rotación del cubo
    [SerializeField] private LayerMask floor;
    [SerializeField] GameObject body;
    public bool near;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private Vector3 ScaleDetect;
    [SerializeField] private GameObject check;
     private PlayerControllers playerControllers;

    void Start()
    {
        playerControllers = GetComponent<PlayerControllers>();

        if (NearGround())
        {
            isGrounded = true;
        }


        //rb = GetComponent<Rigidbody>();
        //isGrounded = false;
    }


    void Update()
    {
        near = NearGround();

        if ( isGrounded && NearGround() )
        {
           
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Jump();
            }
           
        }

        // Saltar cuando se presiona la tecla de espacio y el jugador está en el suelo
        if (  isGrounded && NearGround() && !playerControllers.DetectOrb())
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
            {
                Jump();
            }
          
        }

        // Rotar el cubo cuando está en el aire
        if (!NearGround() && !isGrounded)
        {
            if (Physics.gravity.y < 0)
            {
              body.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
            else
            {
              body.transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
            }
                

        }


    }

    void Jump()
    {
        if(Physics.gravity.y>0) 
        {
            rb.velocity = new Vector3(0, -jumpForce, 0);

        }else
        {
           rb.velocity = new Vector3(0, jumpForce, 0);
        }
         //isGrounded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Marcar al jugador como en el suelo cuando colisiona con un objeto etiquetado como "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            if(body != null)
            {
                Vector3 Rotation = body.transform.rotation.eulerAngles;

                Rotation.x = Mathf.Round(Rotation.x / 90) * 90;
                // DOTween.KillAll(body.transform);
                 body.transform.DORotate(Rotation, 0.05F); ;
               // body.transform.rotation = Quaternion.Euler(Rotation);
            }
            

           
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            
            
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("perroo");
        }

    }

    //private bool NearGround()
    //{
    //    RaycastHit hit;

    //    if (Physics.gravity.y > 0)
    //    {
    //        if (Physics.Raycast(transform.position, Vector3.up, out hit, 0.7f))
    //        {
    //            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Floor"))
    //            {
                    
    //                return true;
    //            }
    //        }

    //    }
    //    else
    //    {
    //        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.7f))
    //        {
    //            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Floor"))
    //            {
    //                return true;
    //            }
    //        }
    //    }

    //    return false;
    //}

    private bool NearGround()
    {
        Collider[] colliders;
        if (Physics.gravity.y > 0)
        {
            check.gameObject.transform.position  = new Vector3( transform.position.x , transform.position.y +  0.464f,transform.position.z);
            colliders = Physics.OverlapBox(check.transform.position , ScaleDetect * 2, Quaternion.identity, floor);
        }
        else
        {
            check.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y +  -0.464f, transform.position.z);
            colliders = Physics.OverlapBox(check.transform.position, ScaleDetect * 2, Quaternion.identity, floor);
        }

            

        return colliders.Length > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (Physics.gravity.y > 0)
        {

            Gizmos.DrawWireCube(check.transform.position , ScaleDetect);
        }
        else
        {
            Gizmos.DrawWireCube(check.transform.position , ScaleDetect);
        }
  
    }


}
