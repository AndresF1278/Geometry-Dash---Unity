using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishLevel : MonoBehaviour
{
    public float pullDuration= 3f; // Fuerza con la que el agujero negro atraerá al jugador
    [SerializeField] Transform PointPosition;
    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            bool DownVolume;
            if (AudioManager.instance.musicSource.volume >= 0.3f)
            {
                AudioManager.instance.musicSource.volume -= 0.2f;
                DownVolume = true;
            }
            else 
            {
            DownVolume = false;
            }


            
            AudioManager.instance.playSfx("Win");
            GameObject.FindAnyObjectByType<CameraFollow>().ChangeTarget(null);
            other.transform.DOMove(PointPosition.position, pullDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
             {


                 //GameManager.Instance.TriggerResetLevel();
                 other.GetComponent<PlayerControllers>().PlayerWinEffect();
                 if(DownVolume) 
                 {
                     AudioManager.instance.musicSource.volume += 0.2f;
                 }
                 StartCoroutine(TimeToBackToInit());

             });
        }
    }
    IEnumerator TimeToBackToInit()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.TriggerMainMenu();
    }
  
    
    
}
