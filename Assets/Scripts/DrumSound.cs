using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumSound : MonoBehaviour
{
    public AudioSource sound;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DrumTop")
        {
            sound.Play();
        }
    }
}
