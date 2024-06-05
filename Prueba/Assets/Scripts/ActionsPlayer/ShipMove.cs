using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour
{
    [SerializeField] float speed;
    private Rigidbody rb;
    [SerializeField] GameObject body;
    [SerializeField] GameObject bodyCube;
    [SerializeField] PlayerControllers playerController;


    private void Start()
    {
        PortalManager.instance.currentVehicleType = typesPortal.portalShip;
        playerController = GetComponent<PlayerControllers>();
         rb = GetComponent<Rigidbody>();        
    }
    private void Update()
    {




        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
        {

                if (Physics.gravity.y > 0)
                {
                    rb.velocity += new Vector3(0, -speed * Time.deltaTime, 0);
                }
                else
                {
                    rb.velocity += new Vector3(0, speed * Time.deltaTime, 0);
                }


        }
        //rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -18, 18), rb.velocity.z);
        body.transform.rotation = Quaternion.Euler(rb.velocity.y, 180f, transform.rotation.z);

        if (GetComponent<PlayerControllers>().gravityNormal)
        {
            body.transform.rotation = Quaternion.Euler(rb.velocity.y, 180, 0);
            bodyCube.transform.localPosition = new Vector3(0.081f, 0.291f, -0.072f);
            bodyCube.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else
        {
            body.transform.rotation = Quaternion.Euler(rb.velocity.y, 180, -180f);
            bodyCube.transform.localPosition = new Vector3(-0.074f, -0.416f, -0.128f);
            bodyCube.transform.rotation = Quaternion.Euler(0, 180, -180);
        }

    }

}
