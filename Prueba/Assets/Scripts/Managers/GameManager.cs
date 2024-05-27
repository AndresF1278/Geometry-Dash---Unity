using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject prefabCube, prefabShip, prefabBall;

    [SerializeField] private int numDeath = 0;

    public int NumDeath => numDeath; 

    private void Awake(){
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
            return;
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

    public void ResetLevel()
    {
        numDeath++;
        Debug.LogWarning($"INTENTO NUMERO: {numDeath}");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
