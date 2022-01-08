using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinueButton : PhysicsButton
{
    // Update is called once per frame
    void LateUpdate()
    {
        if (!GamePaused)
        {
            var colls = Physics.OverlapSphere(transform.position, 5000f);

            foreach(var coll in colls)
            {
                if (coll.GetComponentInParent<MovingTarget>() != null)
                    coll.GetComponentInParent<MovingTarget>().enabled = true;
                else if (coll.GetComponentInParent<StillTargetSpawner>() != null)
                    coll.GetComponentInParent<StillTargetSpawner>().enabled = true;
                else if (coll.GetComponent<TargetSpawner>() != null)
                    coll.GetComponent<TargetSpawner>().enabled = true;
                else if (coll.GetComponent<MLAgent>() != null)
                    coll.GetComponent<MLAgent>().enabled = true;
                else if (coll.GetComponent<Projectile>() != null)
                    coll.GetComponent<Projectile>().enabled = true;
                else if (coll.GetComponent<Shotgun>() != null)
                    coll.GetComponent<Shotgun>().enabled = true;
            }
        }
    }

    protected override void Pressed()
    {
        base.Pressed();
        GamePaused = false;
    }
}
