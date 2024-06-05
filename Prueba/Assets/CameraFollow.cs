using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float offset;


    private void Start()
    {
        if(target == null)
        {
            target = GameObject.FindAnyObjectByType<PlayerControllers>().transform;
        }
       
    }


    private void Update()
    {
        if(target != null)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z + offset);
        }
      

    }
     
    public void ChangeTarget(Transform objectTrack)
    {
        target = objectTrack;
    }
}
