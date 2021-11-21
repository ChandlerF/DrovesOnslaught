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
            col.GetComponent<Health>().Damage(Damage);

            //set particle's x rot = my (bullet) z rot
            //So the particle explodes away from enemy (rather than disappearing inside the enemy)
            Quaternion ParticleRot = Quaternion.Euler(transform.eulerAngles.z, -90, 0);
            Instantiate(ColParticles, transform.position, ParticleRot); 

            if (DestroyOnCol)
            {
                Destroy(gameObject);
            }
        }
    }
}
