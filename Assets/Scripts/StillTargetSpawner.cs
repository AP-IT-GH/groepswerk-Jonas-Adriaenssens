using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StillTargetSpawner : MonoBehaviour
{
    public float TTL = 5;
    public float OutlineTime = 3;
    private float Spawned = 0;
    public Vector3 test = Vector3.zero;
    [SerializeField]
    private Outline outline;


    // Start is called before the first frame update
    void Start()
    {
        // transform.parent.rotation = Quaternion.Euler(-Random.Range(10, 180), Random.Range(0, 360), 0);
    }


        private void OnEnable()
    {
        if(outline == null)
        {
            outline = GetComponentInChildren<Outline>();
        }
        outline.enabled = (false);
        Spawned = Time.time;

        transform.localPosition = new Vector3(-25, 0, 0);
        transform.localRotation = Quaternion.identity;
        transform.parent.localRotation = Quaternion.identity;


        transform.parent.Rotate(Vector3.up, Random.Range(90, 270));
        transform.parent.Rotate(Vector3.forward * -Random.Range(10, 30));

        
        //transform.parent.localRotation = Quaternion.Euler(0, Random.Range(170, 190), 0);
        //transform.parent.RotateAround(transform.parent.position, Vector3.up, Random.Range(-5, 5)); 
        //transform.LookAt(transform); 
        //transform.RotateAround(transform.parent.position, transform.parent.up, );
    }


    // Update is called once per frame
    void Update()
    {
        transform.parent.Rotate(test * Time.deltaTime);
        if(Time.time - Spawned > OutlineTime && outline.enabled == false)
        {
            outline.enabled = true;
        }
        if (Time.time - Spawned > TTL)
        {
            gameObject.SetActive(false); 
        }
    }
}
