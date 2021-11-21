using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//need to put on start for buildings
//the same things that i do when I spawn it
public class BuildingChecker : MonoBehaviour
{
    public bool TouchingBuilding = false;

    public bool TouchingOre = false;

    public GameObject Building = null;

    private bool DestroyOres = false;

    private Arrays ListScript;

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 30;    // it's this 30 + the camera's -30
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);


        transform.position = worldPosition;   ///////////////////

        //Click mouse, not touching buildings, game isn't paused
        if(Input.GetMouseButtonDown(0) && !TouchingBuilding && Time.timeScale != 0)     /////////////////////
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
                    
                    ListScript.InPlacingBuildingMode = false;
                    Destroy(gameObject);
                }
            }
        }



        if (Input.GetMouseButtonDown(1))        ///////////////////////////
        {
            Arrays ListScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Arrays>();
            ListScript.ChangeButtonsActive();

            ListScript.InPlacingBuildingMode = false;
            Destroy(gameObject);
        }
    }





    private void SpawnBuilding()
    {
        GameObject Manager = GameObject.FindGameObjectWithTag("Manager");
        ListScript = Manager.GetComponent<Arrays>();
        PlacingBuildings PlaceBuilding = Manager.GetComponent<PlacingBuildings>();

        Vector3 SpawnPos = new Vector3(transform.position.x, transform.position.y, 0);

        //Spawn building
        GameObject SpawnedBuilding = Instantiate(Building, SpawnPos, Building.transform.rotation);
        //Add building to list (of all buildings)
        ListScript.BuildingsList.Add(SpawnedBuilding);
        //Add building to it's individual list in the dictionary
        string SpawnedName = SpawnedBuilding.gameObject.name;
        ListScript.BuildingDict[SpawnedName.Remove(SpawnedName.Length - 7)].Add(SpawnedBuilding);   //Removes lasts 7 letters, "(Clone)"

        ButtonInfo BuildingInfo = SpawnedBuilding.GetComponent<ButtonInfo>();

        //Reference the visual gameobject
        GameObject Visual = PlaceBuilding.Visual;
        //Spawn visual
        GameObject SpawnedVisual = Instantiate(Visual, transform.position, Building.transform.rotation);

        //Set building's visual
        BuildingInfo.RangeVisual = SpawnedVisual;

        //Find size to scale visual up to
        float Scale = Mathf.Sqrt(SpawnedBuilding.GetComponent<FindEnemies>().MaxRange) * 2;
        //Set visual scale
        SpawnedVisual.transform.localScale = new Vector3(Scale, Scale, Scale);
        //Building pos = Visual pos
        SpawnedBuilding.transform.position = SpawnedVisual.transform.position;



        //Add visual to list
        ListScript.VisualsList.Add(SpawnedVisual);


        //Every building re does line renderer
        PlaceBuilding.SettingLineRenderers();
        //Take from player scrap
        Manager.GetComponent<Player>().Scrap -= BuildingInfo.Cost;

        //Manager.GetComponent<Player>().CameraShake(0.1f);

        //Turn buttons on / off
        ListScript.ChangeButtonsActive();
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
            if (col.gameObject.layer == 10)
            {
                Destroy(col.gameObject);
            }
            
            ListScript.InPlacingBuildingMode = false;
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
