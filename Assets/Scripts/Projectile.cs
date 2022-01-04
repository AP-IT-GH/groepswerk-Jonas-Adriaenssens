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

            if (weapon.gameObject.tag == "AI")
            {
                weapon.gameObject.GetComponent<MLAgent>().Miss();
            }

            DestroyObject(gameObject);
        }
    }




    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Target")
        {
            if(weapon.gameObject.tag == "AI")
            {
                weapon.gameObject.GetComponent<MLAgent>().hit();
                ScoreKeeper.instance.playerHit(); 
            } else

            {
                ScoreKeeper.instance.playerHit(); 
            }

            collision.transform.gameObject.SetActive(false); 
            Destroy(gameObject);       
           
        }

    }



}
