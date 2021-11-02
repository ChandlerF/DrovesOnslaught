using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAndKnockback : MonoBehaviour
{
    [SerializeField] int Dmg;

    [SerializeField] int KnockbackForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Weapon") || collision.transform.CompareTag("Producer") || collision.transform.CompareTag("Factory") || collision.transform.CompareTag("Tower"))
        {
            Knockback(KnockbackForce);
            collision.transform.GetComponent<Health>().Damage(Dmg);
        }
    }


    private void Knockback(int Force)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Vector3 TargetPos = GetComponent<MoveTowards>().Target.transform.position;
        Vector3 Dir = TargetPos - transform.position;

        rb.AddForce(Dir.normalized * -Force);
    }
}
