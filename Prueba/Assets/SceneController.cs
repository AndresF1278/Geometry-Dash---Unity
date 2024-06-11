using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameManager.Instance.StartLevel += GoToLevel;
        GameManager.Instance.ResetLevel += GoToLevelOne;
        GameManager.Instance.MainMenu += GoToMenu;



    }

    public void GoToLevelOne()
    {
        SceneManager.LoadScene("Level1");
        AudioManager.instance.musicSource.clip = null;
        AudioManager.instance.PlayMusic("Level1", true);
        
    }

    IEnumerator TimeToScene()
    {
        yield return new WaitForSeconds(0.5f);
        GoToLevelOne();
        AudioManager.instance.musicSource.clip = null;
       AudioManager.instance.PlayMusic("Level1", true);
    }


    private void GointToMainMenu()
    {
        SceneManager.LoadScene("Init");
    }

    IEnumerator TimeToSceneMenu()
    {
        yield return new WaitForSeconds(0.5f);
        GointToMainMenu();
        AudioManager.instance.musicSource.clip = null;
        AudioManager.instance.PlayMusic("Level1", true);
    }

    private void GoToMenu()
    {
        StartCoroutine(TimeToSceneMenu());
    }


    private void GoToLevel()
    {
        StartCoroutine(TimeToScene());
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
