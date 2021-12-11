using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAndKnockback : MonoBehaviour
{
    [SerializeField] int Dmg;
    [SerializeField] GameObject ColParticles;

    public int KnockbackForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        {

            Vector3 TargetPos = GetComponent<MoveTowards>().Target.transform.position;
            Vector3 Dir = TargetPos - transform.position;

            Knockback(KnockbackForce, Dir);

            //Rotation for particles
            Quaternion ParticleRot = Quaternion.Euler(transform.eulerAngles.z, -90, 0);
            //Spawn particles
            Instantiate(ColParticles, transform.position, ParticleRot);

            collision.transform.GetComponent<Health>().Damage(Dmg);
        }
    }


    public void Knockback(float Force, Vector3 Direction)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.AddForce(Direction.normalized * -Force);
        //rb.AddTorque((Force * Mathf.Deg2Rad) * rb.inertia, ForceMode2D.Impulse);
        float rotation = Random.Range(-10, 10);
        transform.Rotate(0, 0, rotation);
    }
}
