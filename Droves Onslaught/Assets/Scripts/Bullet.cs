using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] int Damage;

    [SerializeField] bool DestroyOnCol;

    [SerializeField] GameObject ColParticles;

    void Start()
    {
        Destroy(gameObject, 6f);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            //Damage
            col.GetComponent<Health>().Damage(Damage);

            if (col.GetComponent<Health>().HP > 0)
            {
                //set particle's x rot = my (bullet's) z rot
                //So the particle explodes away from enemy (rather than disappearing inside the enemy)

                Quaternion ParticleRot = Quaternion.Euler(transform.eulerAngles.z, -90, 0);
                //Spawn particles
                Instantiate(ColParticles, transform.position, ParticleRot);


                DamageAndKnockback ColScript = col.transform.GetComponent<DamageAndKnockback>();
                //lesser version of knockback
                float NewForce = ColScript.KnockbackForce * 0.2f;

                //Direction
                Vector3 Dir = col.transform.position - transform.position;

                //knockback
                ColScript.Knockback(NewForce, -Dir);

            }




            if (DestroyOnCol)
            {
                Destroy(gameObject);
            }
        }
    }
}
