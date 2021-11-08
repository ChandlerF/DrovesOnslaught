using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    [SerializeField] string Name;
    //GO target, use Target, not tag to compare col


    //Tether mode is set true but never false
    //BuildingButton, have it spawn upgrade menu, and set upgrade buttons active as needed
    //Feedback to know you're in Tether Mode (Selected Building's line renderer target is mouse or, because mobile, dim screen with overlay)
    //Add World Canvas to other buildings, like transport
    //Bug where if you're placing a building, and click another building instead of placing it, bad

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
