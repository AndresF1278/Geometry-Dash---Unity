using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening; 

public class CanvasReset : MonoBehaviour
{
    [SerializeField] string attemptText;
    [SerializeField] TMP_Text _textMeshPro;

    void Start()
    {
        int num = GameManager.Instance.NumDeath;
        _textMeshPro.text = attemptText + num.ToString();
    }




}