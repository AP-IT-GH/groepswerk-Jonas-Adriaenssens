using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawn : MonoBehaviour
{
    public GameObject Env;
    public int X;
    public int Y;

    public float Offset = 600;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                Vector3 spawn = new Vector3(i * Offset, 0, j * Offset);
                GameObject go = Instantiate(Env, spawn, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
