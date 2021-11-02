using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParent : MonoBehaviour
{
    private GameObject Manager;
    private Arrays ListScript;
    private PlacingBuildings PlaceBuildings;

    public GameObject SelectedBuilding; //Set by BuildingButton when it's spawned

    private void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("Manager");
        ListScript = Manager.GetComponent<Arrays>();
        PlaceBuildings = Manager.GetComponent<PlacingBuildings>();
    }

    public void DestroyTheParent()
    {
        ListScript.ChangeButtonsActive();

        Destroy(transform.parent.gameObject);
    }


    public void SpawnBuilding(GameObject go)        //Called by button
    {
        PlaceBuildings.ClickedButton(go);
    }


    public void ReplaceBuilding()
    {
        GameObject NewBuilding = GetComponent<SetButton>().Building;

        GameObject SpawnedBuilding = Instantiate(NewBuilding, SelectedBuilding.transform.position, SelectedBuilding.transform.rotation);

        Manager.GetComponent<Arrays>().BuildingsList.Add(SpawnedBuilding);


        ListScript.ChangeButtonsActive();


        //Destroy building
        //Spawn new building in the same spot
        //And maybe reset line renderes if you make them manual












        Manager.GetComponent<Arrays>().BuildingsList.Remove(SelectedBuilding);
        //Add building to list
        Manager.GetComponent<Arrays>().BuildingsList.Add(SpawnedBuilding);


        //Find size to scale visual up to
        float Scale = Mathf.Sqrt(SpawnedBuilding.GetComponent<FindEnemies>().MaxRange) * 2;
        //Set visual scale
        SelectedBuilding.GetComponent<ButtonInfo>().RangeVisual.transform.localScale = new Vector3(Scale, Scale, Scale);
       



        //Every building re does line renderer
        Manager.GetComponent<PlacingBuildings>().SettingLineRenderers();
        //Take from player scrap
        Manager.GetComponent<Player>().Scrap -= NewBuilding.GetComponent<ButtonInfo>().Cost;



        Destroy(SelectedBuilding);

        //Turn buttons on / off
        ListScript.ChangeButtonsActive();

        DestroyTheParent();

    }
}
