using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamage
{
    void ApplyDamage(Weapon weapon, Projectile projectile, Vector3 contactPoint);
}
