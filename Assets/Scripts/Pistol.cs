using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pistol : XRBaseInteractable
{
    public GameObject bulletprefab;
    public Transform bulletSpawn;


    private Rigidbody rigidBody;
    private XRGrabInteractable interactableweapon;



    protected override void Awake()
    {
        base.Awake();
        interactableweapon = GetComponent<XRGrabInteractable>();
        rigidBody = GetComponent<Rigidbody>();
        SetupInteractatableWeaponevent();

    }


    private void SetupInteractatableWeaponevent()
    {
        interactableweapon.onActivate.AddListener(StartShooting);
        interactableweapon.onDeactivate.AddListener(StopShooting);
    }

    private void StopShooting(XRBaseInteractor interactor)
    {
    }

    private void StartShooting(XRBaseInteractor interactor)
    {
        GameObject projectileInstance = Instantiate(bulletprefab, bulletSpawn.position, bulletSpawn.rotation);

    }

   

}
