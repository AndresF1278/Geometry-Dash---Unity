using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class CubeNew : MonoBehaviour
{
    [SerializeField] float forceJump;
    [SerializeField] LayerMask groundMask;
    Rigidbody rb;
    [SerializeField] GameObject body;
    [SerializeField] Transform groundCheck;
    [SerializeField] bool tocapiso;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        tocapiso = OnGround();
        if (OnGround())
        {

            Vector3 Rotation = body.transform.rotation.eulerAngles;

            Rotation.x = Mathf.Round(Rotation.x / 90) * 90;
            // DOTween.KillAll(body.transform);
            body.transform.DORotate(Rotation, 0.05F); ;
            //body.transform.rotation = Quaternion.Euler(Rotation);

            if (Input.GetMouseButton(0))
            {
                rb.velocity = Vector3.zero;
                jump();
            }
        }
        else
        {
            body.transform.Rotate(Vector3.up * 400 * Time.deltaTime);
        }
    }

    void jump()
    {
        if (Physics.gravity.y > 0)
        {
            rb.velocity = new Vector3(0, -forceJump, 0);

        }
        else
        {
            rb.velocity = new Vector3(0, forceJump, 0);
        }
    }

    bool OnGround()
    {
       Collider[] colli =  Physics.OverlapBox(groundCheck.position, new Vector3(0, 0.8f, 1.8f), Quaternion.identity, groundMask);  
        return colli.Length > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = OnGround() ? Color.green : Color.red;
        Gizmos.DrawWireCube(groundCheck.position, new Vector3(0,0.5f,1) );
    }


    private bool NearGround()
    {
        RaycastHit hit;

        if (Physics.gravity.y > 0)
        {
            if (Physics.Raycast(transform.position, Vector3.up, out hit, 0.7f))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Floor"))
                {

                    return true;
                }
            }

        }
        else
        {
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.7f))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Floor"))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
