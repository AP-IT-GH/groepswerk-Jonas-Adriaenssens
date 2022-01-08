using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StillTargetSpawner : MonoBehaviour
{
    public float TTL = 5;
    private float Spawned = 0;


    // Start is called before the first frame update
    void Start()
    {
        transform.parent.rotation = Quaternion.Euler(-Random.Range(10, 180), Random.Range(0, 360), 0);
    }


        private void OnEnable()
    {
        Spawned = Time.time;

        transform.localPosition = new Vector3(-25, 0, 0);
        transform.localRotation = Quaternion.identity;
        transform.parent.rotation = Quaternion.Euler(-Random.Range(10, 180), Random.Range(0, 360), 0);
        transform.RotateAround(transform.parent.position, transform.parent.up, Random.Range(15, 150));
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
