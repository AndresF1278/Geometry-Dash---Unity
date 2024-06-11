using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    [SerializeField] private GameObject CanvasReset;
    [SerializeField] private GameObject CanvasInitial;

    [Space]
    [Header("ButtonsCanvasInitial")]
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button ExitButton;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider SliderSFX;

    [Space]
    [Header("ButtonsCanvasFinish")]
    [SerializeField] private Button ResetButton;
    [SerializeField] private Button HomeButton;
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Slider sliderMusicFinish;
    [SerializeField] private Slider SliderSFXFinish;
    [Space]
    [Header("Fade")]
    [SerializeField] Image ImageFade;


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
    private void Start()
    {
        GameManager.Instance.StartLevel += Fade;
        GameManager.Instance.MainMenu += Fade;
        GameManager.Instance.StartLevel += TriggerHideCanvas;
        GameManager.Instance.ResetLevel += HideAllCanvas;
        GameManager.Instance.MainMenu += HideResetCanvas;
        GameManager.Instance.MainMenu += TriggerShowCanvas;

        

        ButtonsConfig();

    }


    public void HideAllCanvas()
    {
        CanvasReset.SetActive(false);
        CanvasInitial.SetActive(false);

        

    }
    public void HideResetCanvas()
    {
        CanvasReset.SetActive(false);
  
    }

    public void ShowCanvasPause()
    {
        CanvasReset.SetActive(true);
        CanvasReset.GetComponent<GraphicRaycaster>().enabled = true;
        SliderSFXFinish.value = AudioManager.instance.sfxSource.volume;
        sliderMusicFinish.value =  AudioManager.instance.musicSource.volume;
    }


    public void ShowCanvasInit()
    {
        
        CanvasReset.SetActive(false);
        CanvasInitial.SetActive(true);
        CanvasInitial.GetComponent<GraphicRaycaster>().enabled = true;
        sliderMusic.value = AudioManager.instance.musicSource.volume;
        SliderSFX.value = AudioManager.instance.sfxSource.volume;


    }
    IEnumerator TimeToShowCanvas()
    {

        yield return new WaitForSeconds(0.5f);
        ShowCanvasInit();
    }


    IEnumerator TimeToHideCanvas()
    {
        CanvasInitial.GetComponent<GraphicRaycaster>().enabled = false;
        CanvasReset.GetComponent<GraphicRaycaster>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        HideAllCanvas();
    }


    public void TriggerHideCanvas()
    {
        StartCoroutine(TimeToHideCanvas());
    }
    public void TriggerShowCanvas()
    {
        StartCoroutine(TimeToShowCanvas());
    }

    private void ButtonsConfig()
    {
        PlayButton.onClick.AddListener(GameManager.Instance.TriggerStartLevel);
        ExitButton.onClick.AddListener(GameManager.Instance.QuitGame);


        ResetButton.onClick.AddListener(GameManager.Instance.TriggerResetLevel);
        ResumeButton.onClick.AddListener(GameManager.Instance.Resume);
        HomeButton.onClick.AddListener(GameManager.Instance.TriggerMainMenu);

        sliderMusic.onValueChanged.AddListener(AudioManager.instance.MusicVolume);
        sliderMusicFinish.onValueChanged.AddListener(AudioManager.instance.MusicVolume);

        SliderSFX.onValueChanged.AddListener(AudioManager.instance.SFXVolume);
        SliderSFXFinish.onValueChanged.AddListener(AudioManager.instance.SFXVolume);
    }

 

  
    public void Fade()
    {
        ImageFade.DOFade(1, 0.5f).OnComplete(() =>
        {
            
            ImageFade.DOFade(0, 0.5f);
        });

    }

}
