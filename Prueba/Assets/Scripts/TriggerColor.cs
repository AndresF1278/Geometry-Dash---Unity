using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerColor : MonoBehaviour
{
    [SerializeField] Color FinalColor;
    [SerializeField] float Time;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Camera.main.GetComponent<BackgroundGradientDOTween>().ChangeColor(FinalColor, Time);
        }

    }

}
