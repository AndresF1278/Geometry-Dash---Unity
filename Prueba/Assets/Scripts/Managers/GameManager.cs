using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject prefabCube, prefabShip, prefabBall;

    [SerializeField] private int numDeath = 1;

    public int NumDeath => numDeath;

    public delegate void EventStartLevel();
    public event EventStartLevel StartLevel;

    public delegate void EventResetLevel();
    public event EventStartLevel ResetLevel;

    public delegate void EventMainMenu();
    public event EventStartLevel MainMenu;

    public bool IsPaused;

    public void TriggerStartLevel()
    {
        if(StartLevel != null)
        {
            StartLevel();
        }   
    }
    public void TriggerResetLevel()
    {
        if (ResetLevel != null)
        {
            
            numDeath++;
            OnPause(false);
            ResetLevel();
        }
    }

    public void TriggerMainMenu() 
    {
        if (MainMenu != null)
        {

            numDeath = 0;
            GameObject.FindAnyObjectByType<PlayerControllers>().enabled = false;
            OnPause(false);
            MainMenu();
        }
    }

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
    public void Resume()
    {
        CanvasManager.instance.HideAllCanvas();
        AudioManager.instance.musicSource.UnPause();
        OnPause(false);
    }



    public void OnPause(bool pause = true){

        if (!pause)
        {
            Time.timeScale = 1f;
            IsPaused = false;
        }
        else
        {
            Time.timeScale = 0f;
            IsPaused = true;
        }
           
    }

    public void FinishLevel(){

        OnPause();
    }

    public void QuitGame()
    {
        // Si está ejecutándose en el Editor de Unity
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
        #else
                // Si está ejecutándose como un build (juego real)
                Application.Quit();
        #endif
    }


}
