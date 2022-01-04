using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Rigidbody body;
    private IEnumerator coroutine;

    public float Speed = 15;
    public float TimeToLive = 5;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        body.velocity = transform.forward * Speed;
        coroutine = DestoryOverTime();
        StartCoroutine(coroutine);
    }

    IEnumerator DestoryOverTime()
    {
        yield return new WaitForSeconds(TimeToLive);
        gameObject.SetActive(false);
    }
}
