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
        Manager.GetComponent<Player>().Scrap += Scrap;  


        List<GameObject> gos = Manager.GetComponent<Arrays>().BuildingsList; 
        if (gos.Contains(gameObject))
        {
            Manager.GetComponent<Player>().CameraShake(0.5f);
            gos.Remove(gameObject);
        }

        if (transform.CompareTag("Tower"))
        {
            Time.timeScale = 0;
        }

        if (GetComponent<ButtonInfo>())
        {
            //Remove it from list
            Destroy(GetComponent<ButtonInfo>().RangeVisual);
        }



        Manager.GetComponent<Arrays>().BuildingDict[gameObject.name.Remove(gameObject.name.Length - 7)].Remove(gameObject);

        Instantiate(Particles, transform.position, Particles.transform.rotation);
        Destroy(gameObject);
    }
}
