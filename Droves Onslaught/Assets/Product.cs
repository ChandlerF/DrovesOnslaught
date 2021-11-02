using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    [SerializeField] string Name;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(Name == "Ammo")
        {
            if (col.CompareTag("Weapon"))
            {
                col.GetComponent<WeaponShooting>().Ammo += 1;
                Destroy(gameObject);
            }

            else if (col.CompareTag("Transport"))
            {
                col.GetComponent<Producer>().ProductInStock += 1;
                Destroy(gameObject);
            }
        }
        else if(Name == "Ore")
        {
            if (col.CompareTag("Factory"))
            {
                col.GetComponent<Producer>().ProductInStock += 1;
                Destroy(gameObject);
            }
        }
        else if(Name == "Points")
        {
            if (col.CompareTag("Tower"))
            {
                col.GetComponent<Tower>().Score += 1;
                Destroy(gameObject);
            }
        }
    }
}
