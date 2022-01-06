using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingAider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnCollisionStay(Collision collision)
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            Debug.Log("aid collided with target"); 
            
        }
    }


}
