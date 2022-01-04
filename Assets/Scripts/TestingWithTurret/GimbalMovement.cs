using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimbalMovement : MonoBehaviour
{
    public float rotationSpeed; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, vertical * rotationSpeed * Time.deltaTime);
        
    }
}
