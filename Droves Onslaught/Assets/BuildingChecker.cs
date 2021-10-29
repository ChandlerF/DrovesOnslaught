using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingChecker : MonoBehaviour
{
    public bool TouchingBuilding = false;

    public bool TouchingOre = false;

    public GameObject Building = null;

    private bool DestroyOres = false;

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);


        transform.position = worldPosition;   ///////////////////

        if(Input.GetMouseButtonDown(0) && !TouchingBuilding)     /////////////////////
        {
            if (Building.CompareTag("Producer"))
            {
                if (TouchingOre)
                {
                    DestroyOres = true;

                    SpawnBuilding();
                }
            }

            else
            {
                if (!TouchingOre)
                {
                    SpawnBuilding();
                    Destroy(gameObject);
                }
            }
        }



        if (Input.GetMouseButtonDown(1))        ///////////////////////////
        {
            Destroy(gameObject);
        }
    }





    private void SpawnBuilding()
    {
        GameObject Manager = GameObject.FindGameObjectWithTag("Manager");

        GameObject SpawnedBuilding = Instantiate(Building, transform.position, Building.transform.rotation);
        Manager.GetComponent<Arrays>().BuildingsList.Add(SpawnedBuilding);


        GameObject Visual = Manager.GetComponent<PlacingBuildings>().Visual;
        GameObject SpawnedVisual = Instantiate(Visual, transform.position, Building.transform.rotation);

        float Scale = Mathf.Sqrt(SpawnedBuilding.GetComponent<FindEnemies>().MaxRange) * 2;

        SpawnedVisual.transform.localScale = new Vector3(Scale, Scale, Scale);

        SpawnedBuilding.transform.position = SpawnedVisual.transform.position;

        Manager.GetComponent<Arrays>().VisualsList.Add(SpawnedVisual);



        Manager.GetComponent<PlacingBuildings>().SettingLineRenderers();

        Manager.GetComponent<Player>().Scrap -= Building.GetComponent<ButtonInfo>().Cost;
    }


    private void OnTriggerStay2D(Collider2D col)        //Called Every frame of collision
    {
        if (col.gameObject.layer == 6)       //Buildings
        {
            TouchingBuilding = true;
            //Debug.Log("Touching Building");
        }
        else if (col.gameObject.layer == 10)        //Ore
        {
            TouchingOre = true;
            //Debug.Log("Touching Ore");
        }

        if (DestroyOres)        //Called when Building is placed, destroys ore's then the building checker
        {
            Destroy(col.gameObject);

            Destroy(gameObject);
        }
    }




    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 6)       //Buildings
        {
            TouchingBuilding = false;
            //Debug.Log("Not Touching Building");
        }
        else if (col.gameObject.layer == 10)        //Ore
        {
            TouchingOre = false;
           // Debug.Log("Not Touching Ore");
        }
    }
}
