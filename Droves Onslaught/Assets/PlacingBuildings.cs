using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingBuildings : MonoBehaviour
{
    [SerializeField] GameObject BuildingChecker;


    public void ClickedButton(GameObject Building)      //Need to hide UI after clicking
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);


        GameObject SpawnedCheker = Instantiate(BuildingChecker, worldPosition, BuildingChecker.transform.rotation);   ///////////////
        SpawnedCheker.GetComponent<BuildingChecker>().Building = Building.GetComponent<SetButton>().Building;

        SpawnedCheker.GetComponent<SpriteRenderer>().sprite = Building.GetComponent<SetButton>().Building.GetComponent<SpriteRenderer>().sprite;
    }

    private void Update()
    {/*
        if (Input.GetMouseButtonDown(0))
        {
            SettingLineRenderers();
        }*/
    }

    public void SettingLineRenderers()  //For when a building is placed, it's line renderer and target has to be changed
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Producer");        //And factory


        foreach (GameObject go in gos)
        {
            if(go.GetComponent<Producer>().TargetBuilding != null)
            {
                go.GetComponent<Producer>().SetLRPos(go.GetComponent<FindEnemies>().FindClosestEnemy());

                go.GetComponent<Producer>().TargetBuilding = go.GetComponent<FindEnemies>().FindClosestEnemy();

                go.GetComponent<MoveTowards>().Target = go.GetComponent<FindEnemies>().FindClosestEnemy();
            }
        }




        GameObject[] Gos;
        Gos = GameObject.FindGameObjectsWithTag("Factory");


        foreach (GameObject Go in Gos)
        {
            if (Go.GetComponent<Producer>().TargetBuilding != null)
            {
                Go.GetComponent<Producer>().SetLRPos(Go.GetComponent<FindEnemies>().FindClosestEnemy());

                Go.GetComponent<Producer>().TargetBuilding = Go.GetComponent<FindEnemies>().FindClosestEnemy();

                Go.GetComponent<MoveTowards>().Target = Go.GetComponent<FindEnemies>().FindClosestEnemy();
            }
        }
    }
}
