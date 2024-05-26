using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject prefabCube, prefabShip, prefabBall;
    private void Awake(){
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(Instance);
        }
    }


    public void OnPause(bool pause = true){

        if (!pause) 
            Time.timeScale = 1f;
        else
            Time.timeScale = 0f;
    }

    public void FinishLevel(){
        CanvasManager.instance.ShowPanel();
        OnPause();
    }


}
