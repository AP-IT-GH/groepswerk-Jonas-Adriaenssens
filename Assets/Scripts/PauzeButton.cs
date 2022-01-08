using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PauzeButton : PhysicsButton
{
    void LateUpdate()
    {
        if (GamePaused)
        {
            var colls = Physics.OverlapSphere(transform.position, 5000f);
            Debug.Log(colls.Length);
            foreach (var coll in colls)
            {
                Debug.Log(coll.GetComponent<Shotgun>() != null);

                if (coll.GetComponentInParent<MovingTarget>() != null)
                    coll.GetComponentInParent<MovingTarget>().enabled = false;
                else if (coll.GetComponentInParent<StillTargetSpawner>() != null)
                    coll.GetComponentInParent<StillTargetSpawner>().enabled = false;
                else if (coll.GetComponent<TargetSpawner>() != null)
                    coll.GetComponent<TargetSpawner>().enabled = false;
                else if (coll.GetComponent<MLAgent>() != null)
                    coll.GetComponent<MLAgent>().enabled = false;
                else if (coll.GetComponent<Projectile>() != null)
                    coll.GetComponent<Projectile>().enabled = false;
                else if (coll.GetComponent<Shotgun>() != null)
                    coll.GetComponent<Shotgun>().enabled = false;
            }
        }
    }

    protected override void Pressed()
    {
        base.Pressed();
        GamePaused = true;
    }
}
