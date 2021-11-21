using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAndKnockback : MonoBehaviour
{
    [SerializeField] int Dmg;

    public int KnockbackForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        {

            Vector3 TargetPos = GetComponent<MoveTowards>().Target.transform.position;
            Vector3 Dir = TargetPos - transform.position;

            Knockback(KnockbackForce, Dir);


            collision.transform.GetComponent<Health>().Damage(Dmg);
        }
    }


    public void Knockback(float Force, Vector3 Direction)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.AddForce(Direction.normalized * -Force);
    }
}
