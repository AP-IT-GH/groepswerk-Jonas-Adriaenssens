using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public Pooling pool;
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
            var v = pool.GetObject();
            v.transform.position = transform.position;
            v.transform.GetChild(0).gameObject.SetActive(true);
            d();
        }
    }
}
