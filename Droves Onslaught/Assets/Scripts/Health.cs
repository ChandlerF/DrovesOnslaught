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
        GameObject Manager = GameObject.FindGameObjectWithTag("Manager");
        Manager.GetComponent<Player>().Scrap += Scrap;  //Turn this into a function like the Damage() - place on Health script


        List<GameObject> gos = GameObject.FindGameObjectWithTag("Manager").GetComponent<Arrays>().BuildingsList; 
        if (gos.Contains(gameObject))
        {
            Manager.GetComponent<Player>().CameraShake(0.6f);
            gos.Remove(gameObject);
        }

        if (transform.CompareTag("Tower"))
        {
            Time.timeScale = 0;
        }

        Instantiate(Particles, transform.position, Particles.transform.rotation);
        Destroy(gameObject);
    }
}
