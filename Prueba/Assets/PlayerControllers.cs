using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static GameManager;



public class PlayerControllers : MonoBehaviour
{
    [SerializeField] private float speed;
    public bool gravityNormal;
    [SerializeField] private float gravitySpeed;
    [SerializeField] private LayerMask layerOrbs;
    private Orbs currentOrb;
    public GameObject ParticleDeath;
    public List<GameObject>  modelsPlayer;

    public void ChangeGravityPlayer()
    {
        gravityNormal = !gravityNormal;
        Physics.gravity *= -1;
     }


    public void ChangeGravityPortal(bool portalNormal)
    {
        if (portalNormal && !gravityNormal)
        {
            gravityNormal = !gravityNormal;
            Physics.gravity *= -1;

        }

        if (!portalNormal && gravityNormal)
        {
            gravityNormal = !gravityNormal;
            Physics.gravity *= -1;

        }
    }

    private void Start()
    {

        if(Physics.gravity.y > 0)
        {
            gravityNormal = false;
        }
        else
        {
            gravityNormal = true;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (DetectOrb())
            {
                currentOrb.SelectMethodEffect(GetComponent<Rigidbody>());
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Death();
        }
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(0, 0, speed * Time.deltaTime);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Obstacle"))
    //    {
    //        GameManager.Instance.ResetLevel();
    //    }
    //}


    public bool DetectOrb()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, Vector3.one/ 2, Quaternion.identity, layerOrbs);
        
        if(colliders.Length > 0)
        {
            currentOrb = colliders[0].GetComponent<Orbs>();
            return true;
        }
        else
        {
            return false;
        }


        
    }

    public void Death()
    {
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.musicSource.time = 0;
        AudioManager.instance.playSfx("Death");
        foreach (GameObject item in modelsPlayer)
        {
            item.SetActive(false);
        }
        speed = 0;
       GameObject ParticleDeathInstance = Instantiate(ParticleDeath, transform.position, Quaternion.identity);
        
        StartCoroutine(TimeToDeath());

    }
    IEnumerator TimeToDeath()
    {
        yield return new WaitForSeconds(0.6f);
        
        GameManager.Instance.ResetLevel();

        AudioManager.instance.musicSource.Play();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }

}
