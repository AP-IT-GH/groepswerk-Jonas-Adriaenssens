using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Weapon
{
    public GameObject projectile;
    public float projectileSpeed = 100f;
    [SerializeField] private ParticleSystem Flash;
    [SerializeField] private AudioSource sound;

    private float lastShot = 0;

    public void Fire()
    {
        Debug.Log("Shoot.Fire() called");

        if (Time.time - lastShot <= 1f)
        {
            Debug.Log("No shoot");
            return;
        }

        GameObject newProjectile = Instantiate(projectile, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        newProjectile.transform.localScale = newProjectile.transform.localScale * 2f;

        Flash.Play();
        sound.Play();

        var pj = newProjectile.GetComponent<Projectile>();
        pj.Init(this);
        pj.Launch(projectileSpeed);
        lastShot = Time.time;
    }
}
