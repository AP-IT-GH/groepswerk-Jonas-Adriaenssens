using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public Pooling poolMoving;
    public Pooling poolStill;
    public float MinTimeWait = 2;
    public float MaxTimeWait = 5;

    float nextSpawn;

    private void Start()
    {
        d();
    }
    public void d()
    {
        nextSpawn = Time.time + Random.Range(MinTimeWait, MaxTimeWait);
    }

    private void Update()
    {
        if(Time.time > nextSpawn)
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
            d();
        }
    }
}
