using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    [SerializeField] string Name;
    
    public GameObject Target;


    //Tether mode is set true but never false
    //Feedback to know you're in Tether Mode (Selected Building's line renderer target is mouse or, because mobile, dim screen with overlay)
    //Add World Canvas to other buildings, like transport
    //Bug where if you're placing a building, and click another building instead of placing it, bad
    //Can i cancel if I'm placing a building?
    
    //When placing a building, show all visuals for where to place it
    //Do this by setting value in building checker, have it go through dict and grab visuals from each object
    

    private void OnTriggerEnter2D(Collider2D col)
    {
            if (col.gameObject == Target)
            {
                if (col.GetComponent<WeaponShooting>())
                {
                    col.GetComponent<WeaponShooting>().Ammo += 1;
                }
                else if(col.GetComponent<Producer>())
                {
                    col.GetComponent<Producer>().ProductInStock += 1;
                }
                else if(col.GetComponent<Tower>())
                {
                col.GetComponent<Tower>().Score += 1;
                }

                Destroy(gameObject);
            }
    }
}
