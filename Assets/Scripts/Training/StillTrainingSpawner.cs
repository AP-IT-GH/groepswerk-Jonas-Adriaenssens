using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StillTrainingSpawner : MonoBehaviour
{
    public GameObject ObjectToSpawn;
    private float nextActionTime = 0.0f;
    public float period = 2f;


    // Start is called before the first frame update
    void Start()
    {

    }




    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;

            ObjectToSpawn.SetActive(true);

        }
    }

}
