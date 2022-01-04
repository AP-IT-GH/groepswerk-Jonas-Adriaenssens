using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsDamage : MonoBehaviour, ITakeDamage
{
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    public void ApplyDamage(Weapon weapon, Projectile projectile, Vector3 contactPoint)
    {
        Destroy(gameObject);
        //rigidbody.AddForce(projectile.transform.forward * weapon.GetShootingForce(), ForceMode.Impulse);
    }
}
