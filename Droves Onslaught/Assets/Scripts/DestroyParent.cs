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

    private void Update()
    {
        if(SelectedBuilding == null && transform.name == "DeleteButton") //Have it this way because if code runs an even amount then it doesn't work (even because all 4 buttons have this code)
        {
            DestroyTheParent();
        }
    }

    public void DestroyTheParent()
    {
        ListScript.ChangeButtonsActive();

        ListScript.UpgradeButtonActive = false;

        if (SelectedBuilding != null)
        {
            //Disable building visual
            SelectedBuilding.GetComponent<ButtonInfo>().RangeVisual.GetComponent<SpriteRenderer>().enabled = false;
            
            //Destroy instead of diasable and remove from list?
        }

        //Set tether mode false
        ListScript.InTetherMode = false;

        Destroy(transform.parent.parent.gameObject);
    }

    public void DestroySelected()
    {
        ListScript.BuildingDict[SelectedBuilding.name.Remove(SelectedBuilding.name.Length - 7)].Remove(SelectedBuilding);

        ListScript.BuildingsList.Remove(SelectedBuilding);


        Manager.GetComponent<Player>().Scrap += (int) (SelectedBuilding.GetComponent<ButtonInfo>().Cost);

        Destroy(SelectedBuilding);

        Manager.GetComponent<PlacingBuildings>().SettingLineRenderers();

        DestroyTheParent();
    }

    public void SpawnBuilding(GameObject go)        //Called by button
    {
        PlaceBuildings.ClickedButton(go);
    }


    public void ReplaceBuilding()
    {
        GameObject NewBuilding = GetComponent<SetButton>().Building;

        GameObject SpawnedBuilding = Instantiate(NewBuilding, SelectedBuilding.transform.position, SelectedBuilding.transform.rotation);


        ListScript.BuildingsList.Add(SpawnedBuilding);


        ListScript.ChangeButtonsActive();


        //Remove from lists
        ListScript.BuildingDict[SelectedBuilding.name.Remove(SelectedBuilding.name.Length - 7)].Remove(SelectedBuilding);

        ListScript.BuildingsList.Remove(SelectedBuilding);


        //Add building to list
        ListScript.BuildingsList.Add(SpawnedBuilding);

        ListScript.BuildingDict[SpawnedBuilding.name.Remove(SpawnedBuilding.name.Length - 7)].Add(SpawnedBuilding);


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

    public void EnterTetherMode(bool x)   //Called by button
    {
        ListScript.InTetherMode = x;

        //Set ListScript Selected Building to the building that's getting re-tethered, globally
        ListScript.SelectedBuilding = SelectedBuilding;
    }
}
