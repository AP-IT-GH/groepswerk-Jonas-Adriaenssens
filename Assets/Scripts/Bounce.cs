using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public AudioSource audiosource;

    private void OnCollisionEnter(Collision collision)
    {
        audiosource.Play();
    }
}
