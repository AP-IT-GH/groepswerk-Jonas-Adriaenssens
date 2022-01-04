using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    public float MovementSpeed = 50;

    private void Awake()
    {
        transform.parent.rotation = Quaternion.Euler(-Random.Range(10, 180), Random.Range(0, 360), 0);
    }

    private void OnEnable()
    {
        transform.localPosition = new Vector3(-25, 0, 0);
        transform.localRotation = Quaternion.identity;
        transform.parent.rotation = Quaternion.Euler(-Random.Range(10, 180), Random.Range(0, 360), 0);
    }

    private void Update()
    {
        transform.RotateAround(transform.parent.position, transform.parent.up, MovementSpeed * Time.deltaTime);
        if(transform.localPosition.z < -5)
        {
            gameObject.SetActive(false);
        }
    }
}
