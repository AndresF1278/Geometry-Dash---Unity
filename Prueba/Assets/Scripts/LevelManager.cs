using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    public enum TypeVehicleStart
    {
        cube,
        ship,
        ball
    }

    public static LevelManager instance;

    public bool GravityNormal;
    public TypeVehicleStart vehicleStart;
    [SerializeField] private Transform starPos;

    [SerializeField] private GameObject prefabParticleShip, prefabParticleCube, prefabParticleBall;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetGravity();
        SpawnVehicle();
    }

    public void SpawnVehicle()
    {
        GameObject Vehicle;
        switch (vehicleStart)
        {
            case TypeVehicleStart.ball:
                  Vehicle =  Instantiate(GameManager.Instance.prefabBall, starPos.position, Quaternion.identity);
                
                break;
            case TypeVehicleStart.ship:
                Vehicle = Instantiate(GameManager.Instance.prefabShip, starPos.position, Quaternion.identity);
               
                break;
            case TypeVehicleStart.cube:
               Vehicle = Instantiate(GameManager.Instance.prefabCube, starPos.position, Quaternion.identity);
                
                break;

        }

    }

    public void SetGravity()
    {
        if (GravityNormal)
        {
            Physics.gravity = new Vector3(0, -80, 0);
        }
        else { Physics.gravity = new Vector3(0, 80, 0); }
    }
}
