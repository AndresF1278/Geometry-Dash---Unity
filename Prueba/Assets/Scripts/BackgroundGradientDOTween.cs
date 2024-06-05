using UnityEngine;
using DG.Tweening;

public class BackgroundGradientDOTween : MonoBehaviour
{
    public Color startColor = Color.blue;
    //public Color endColor = Color.red;
    //public float duration = 1.0f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCamera.backgroundColor = startColor;
        }
    }
    public void ChangeColor(Color Colorfinal, float Time) 
    {
        mainCamera.DOColor(Colorfinal, Time);
    } 

}
