using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] int HP;

    [SerializeField] GameObject Particles;

    [SerializeField] int Scrap;


    void Update()
    {
        if(HP <= 0)
        {
            Death();
        }
    }


    public void Damage(int Dmg)
    {
        HP -= Dmg;
    }


    private void Death()
    {
        GameObject.FindGameObjectWithTag("Manager").GetComponent<Player>().Scrap += Scrap;  //Turn this into a function like the Damage() - place on Health script

        Instantiate(Particles, transform.position, Particles.transform.rotation);
        Destroy(gameObject);
    }
}
