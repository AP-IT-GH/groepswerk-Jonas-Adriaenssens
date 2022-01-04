using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Shotgun : Weapon
{
    [SerializeField] private Projectile bulletPrefab;
    [SerializeField] private ParticleSystem Flash;
    [SerializeField] private AudioSource sound;
    protected override void StartShooting(ActivateEventArgs arg0)
    {
        Shoot();
    }

    protected override void Shoot()
    {
        base.Shoot();

        Flash.Play();
        sound.Play();

        Projectile projectileInstance = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        projectileInstance.Init(this);
        projectileInstance.Launch();
    }

    protected override void StopShooting(DeactivateEventArgs arg0)
    {

    }

    private void Update()
    {
        var k = Keyboard.current;
        if (k.fKey.wasPressedThisFrame)
        {
            Shoot();
        }
    }
}
