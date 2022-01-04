using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public GameObject Original;
    public int Total = 20;

    private int current = 0;
    private List<GameObject> objects = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < Total; i++)
        {
            var go = GameObject.Instantiate(Original);
            go.SetActive(false);
            go.transform.SetParent(transform);
            objects.Add(go);
        }
    }

    public GameObject GetObject(bool active = true)
    {
        var go = objects[current];
        current = (current + 1) % Total;
        go.SetActive(active);
        return go;
    }
}
