using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    [SerializeField] string Name;
    
    public GameObject Target;



    //Feedback to know you're in Tether Mode (dim screen with overlay), for now the button has a color changed
    //Can i cancel if I'm placing a building?   Right Click, needs to be a button
    
    //When placing a building, show all visuals for where to place it
    //Do this by setting value in building checker, have it go through dict and grab visuals from each object
    
    //Want to be able to drag from button to screen, not just clikc and click, want to drag (for mobile)
    
    //Dedicate portion of the screen to UI (mobile)
    

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
