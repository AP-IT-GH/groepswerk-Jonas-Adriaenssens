using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Weapon weapon;
    [SerializeField] private float TTL = 3;
    private float Spawned;

    public virtual void Init(Weapon weapon)
    {
        this.weapon = weapon;
        Spawned = Time.time;
    }

    public virtual void Launch(float bulletspeed = 100f)
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * bulletspeed, ForceMode.VelocityChange);
    }

    private void Update()
    {
        if(Time.time - Spawned > TTL)
        {
            DestroyObject(gameObject);
        }
    }
}
