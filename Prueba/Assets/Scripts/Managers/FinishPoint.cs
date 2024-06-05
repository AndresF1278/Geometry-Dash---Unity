using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FinishPoint : MonoBehaviour
{
    private BoxCollider m_BoxCollider;

    private void Awake()
    {
        m_BoxCollider = GetComponent<BoxCollider>();
        if (m_BoxCollider != null )
            m_BoxCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.FinishLevel();
            Debug.Log("END LEVEL");
        }
    }
}
