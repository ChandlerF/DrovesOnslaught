using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] int Damage;

    [SerializeField] bool DestroyOnCol;


    void Start()
    {
        Destroy(gameObject, 4f);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Health>().Damage(Damage);

            if (DestroyOnCol)
            {
                Destroy(gameObject);
            }
        }
    }
}
