using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public Pooling poolMoving;
    public Pooling poolStill;
    public float MinTimeWait = 2;
    public float MaxTimeWait = 5;

    [SerializeField]
    private bool TrainingStand = false;
    [HideInInspector]
    public GameObject LastShot;

    float nextSpawn;

    private void Start()
    {
        d();
    }
    public void d()
    {
        nextSpawn = Time.time + Random.Range(MinTimeWait, MaxTimeWait);
    }

    private void Spawn()
    {
        GameObject v;  //=  poolMoving.GetObject();

        //TODO: UNCOMMENT

        /* 
        if (Random.Range(0,100) > 50)
        {
            v = poolMoving.GetObject();
        } else
        {

        */
        v = poolStill.GetObject();
        // }

        v.transform.position = transform.position;
        v.transform.GetChild(0).gameObject.SetActive(true);
        v.GetComponentInChildren<StillTargetSpawner>().TTL = 50000;
        LastShot = v.GetComponentInChildren<StillTargetSpawner>().gameObject;
        d();
    }

    public void Restart()
    {
        LastShot.SetActive(false);
    }
    private void Update()
    {
        // if(Time.time > nextSpawn)
        if(LastShot == null || LastShot.active == false)
        {
            Spawn();
        }
    }
}
