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
            if (weapon != null)
            {

                if (weapon.gameObject.tag == "AI")
                {
                    weapon.gameObject.GetComponent<MLAgent>().Miss();
                }

                Destroy(gameObject);

            }
        }
    }




    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Target")
        {
            Debug.Log("Hit");
            if(weapon.gameObject.tag == "AI")
            {
                weapon.gameObject.GetComponent<MLAgent>().hit();
                ScoreKeeper.instance.aiHit(); 
            } else

            {
                ScoreKeeper.instance.playerHit(); 
            }

            
            if(collision.gameObject.GetComponentInParent<MovingTarget>() != null)
            {
                collision.gameObject.GetComponentInParent<MovingTarget>().gameObject.SetActive(false);
            } else
            {
                collision.gameObject.GetComponentInParent<StillTargetSpawner>().gameObject.SetActive(false);
            }
        }
        Destroy(gameObject);

    }



}
