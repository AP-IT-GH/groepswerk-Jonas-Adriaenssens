using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Unity.MLAgents.Sensors;
public class CameraTexture : MonoBehaviour
{
    public Camera offscreenCamera; // The camera that will do the offscreen rendering  
    public Texture2D curveSelectTexture; // The texture we sample from

    public int nonBackGround = 0;

    private void Start()
    {
        curveSelectTexture = new Texture2D(128, 128);
        StartCoroutine(loop());
    }


    IEnumerator loop()
    {
        while (true)
        {
            //shouldDoRendering = true;
            CameraSensor.ObservationToTexture(offscreenCamera, curveSelectTexture, 128, 128); 
            yield return new WaitForSeconds(1);
        }
    }

    public int GetFromBorder(int border = 0)
    {
        if (curveSelectTexture == null) return -1;
        if (border > curveSelectTexture.width / 2) return -1;

        var nonBackGround = 0;

        for (int x = border; x < curveSelectTexture.width - border; x++)
        {
            for (int y = border; y < curveSelectTexture.height - border; y++)
            {
                Color c = curveSelectTexture.GetPixel(x, y);
                nonBackGround += (c != offscreenCamera.backgroundColor) ? 1 : 0;
            }
        }

        return nonBackGround;
    }
}