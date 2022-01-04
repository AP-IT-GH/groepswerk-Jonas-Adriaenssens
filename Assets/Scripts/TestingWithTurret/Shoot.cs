using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public GameObject projectile;
    public float projectileSpeed = 20f;
    public GameObject bulletSpawn; 

    public void Fire()
    {
        Debug.Log("Shoot.Fire() called");

        GameObject newProjectile = Instantiate(projectile, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);
    }


















}
