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
    public GameObject ParticleWin;
    public List<GameObject>  modelsPlayer;

    [SerializeField] private float timer;
    [SerializeField] private bool timeBool;
    [SerializeField] private float timeBuffer = 0.2f;


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
        timer = 0;
        timeBool = false;

        if (Physics.gravity.y > 0)
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

        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.Instance.IsPaused)
        {
            AudioManager.instance.musicSource.Pause();
            CanvasManager.instance.ShowCanvasPause();
            GameManager.Instance.OnPause();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.IsPaused)
        {
            AudioManager.instance.musicSource.UnPause();
            CanvasManager.instance.HideAllCanvas();
            GameManager.Instance.OnPause(false);
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            timeBool = true;
            timer = 0;

            if (DetectOrb())
            {
                currentOrb.SelectMethodEffect(GetComponent<Rigidbody>());
            }
        }

        if (timeBool)
        {
            timer += Time.deltaTime;

            if (timer < timeBuffer && DetectOrb())
            {
                currentOrb.SelectMethodEffect(GetComponent<Rigidbody>());
                timer = 0;
                timeBool = false;
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
    public void PlayerWinEffect()
    {
        foreach (GameObject item in modelsPlayer)
        {
            item.SetActive(false);
        }
        GameObject ParticleDeathInstance = Instantiate(ParticleWin, transform.position, Quaternion.identity);

    }



    IEnumerator TimeToDeath()
    {
        yield return new WaitForSeconds(0.6f);
        
        GameManager.Instance.TriggerResetLevel();

        AudioManager.instance.musicSource.Play();
    }

    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }

}
