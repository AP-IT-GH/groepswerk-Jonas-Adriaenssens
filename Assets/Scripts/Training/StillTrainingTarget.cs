using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StillTrainingTarget : MonoBehaviour
{
    [SerializeField] private float TTL = 5;
    private float Spawned = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnEnable()
    {
        Spawned = Time.time;
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.time - Spawned > TTL)
        {
            gameObject.SetActive(false);
        }
    }
}
