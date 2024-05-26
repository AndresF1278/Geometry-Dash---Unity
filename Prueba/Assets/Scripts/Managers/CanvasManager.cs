using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    public GameObject principalPanel;

    private void Start()
    {
        HidePanel();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ShowPanel(){
        principalPanel.SetActive(true);
    }

    public void HidePanel() {
        Debug.Log("HidePanel");
        principalPanel.SetActive(false);
    }


    public void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void ResetLevel()
    {
        HidePanel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.Instance.OnPause(false);
    }

    public void HomeLevel()
    {
        SceneManager.LoadScene(0);
    }
}
