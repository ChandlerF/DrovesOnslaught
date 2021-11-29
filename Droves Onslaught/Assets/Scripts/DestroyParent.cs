using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParent : MonoBehaviour
{
    private GameObject Manager;
    private Arrays ListScript;
    private PlacingBuildings PlaceBuildings;

    public GameObject SelectedBuilding; //Set by BuildingButton when it's spawned\

    [SerializeField] GameObject SpawnParticles;
    [SerializeField] GameObject DeathParticles;


    private void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("Manager");
        ListScript = Manager.GetComponent<Arrays>();
        PlaceBuildings = Manager.GetComponent<PlacingBuildings>();
    }

    private void Update()
    {
        if(SelectedBuilding == null && transform.name == "DeleteButton") //Have it this way because if code runs an even amount then it doesn't work (it's even because all 4 buttons have this code)
        {
            DestroyTheParent();
        }
    }

    //Only used once, for upgrade buttons
    public void DisableTheParent()
    {
        ListScript.ChangeButtonsActive();

        ListScript.UpgradeButtonActive = false;

        if (SelectedBuilding != null && SelectedBuilding.GetComponent<ButtonInfo>().RangeVisual != null)
        {
            //Disable building visual
            SelectedBuilding.GetComponent<ButtonInfo>().RangeVisual.GetComponent<SpriteRenderer>().enabled = false;

            //Destroy instead of diasable and remove from list?
        }

        //Set tether mode false
        ListScript.InTetherMode = false;

        transform.parent.parent.gameObject.SetActive(false);
    }

    public void DestroyTheParent()
    {
        ListScript.ChangeButtonsActive();

        ListScript.UpgradeButtonActive = false;

        if (SelectedBuilding != null && SelectedBuilding.GetComponent<ButtonInfo>().RangeVisual != null)
        {
            //Disable building visual
            SelectedBuilding.GetComponent<ButtonInfo>().RangeVisual.GetComponent<SpriteRenderer>().enabled = false;
            
            //Destroy instead of diasable and remove from list?
        }

        //Set tether mode false
        ListScript.InTetherMode = false;

        Destroy(transform.parent.parent.parent.gameObject);
    }

    //Scrap selected building
    public void DestroySelected()
    {
        ListScript.BuildingDict[SelectedBuilding.name.Remove(SelectedBuilding.name.Length - 7)].Remove(SelectedBuilding);

        ListScript.BuildingsList.Remove(SelectedBuilding);

        ListScript.NumOfBuildingsDestroyed += 1;

        //Pop up text when scrapped, refund 75% of cost to make * percentage of hp (so if tower is half health, you get half of 75% back)
        Health HealthScript = SelectedBuilding.GetComponent<Health>();

        float HpPercent = ((float)HealthScript.HP / (float)HealthScript.StartHP);
        HealthScript.SpawnText((int) ((SelectedBuilding.GetComponent<ButtonInfo>().Cost * 0.75) * HpPercent));


        Manager.GetComponent<Player>().CameraShake(0.1f);
        Instantiate(DeathParticles, SelectedBuilding.transform.position, DeathParticles.transform.rotation);


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


        //Set visual reference on the new building
        SpawnedBuilding.GetComponent<ButtonInfo>().RangeVisual = SelectedBuilding.GetComponent<ButtonInfo>().RangeVisual;
        //Set visual scale
        SpawnedBuilding.GetComponent<ButtonInfo>().RangeVisual.transform.localScale = new Vector3(Scale, Scale, Scale);





        //Every building re does line renderer
        Manager.GetComponent<PlacingBuildings>().SettingLineRenderers();
        //Take from player scrap
        Manager.GetComponent<Player>().Scrap -= NewBuilding.GetComponent<ButtonInfo>().Cost;


        Destroy(SelectedBuilding);

        Manager.GetComponent<Player>().CameraShake(0.1f);
        Instantiate(SpawnParticles, SpawnedBuilding.transform.position, SpawnParticles.transform.rotation);

        //Turn buttons on / off
        ListScript.ChangeButtonsActive();

        DisableTheParent();

    }

    public void EnterTetherMode(bool x)   //Called by button
    {
        if(ListScript == null)
        {
            ListScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Arrays>();
        }

        ListScript.InTetherMode = x;

        //Set ListScript Selected Building to the building that's getting re-tethered, globally
        ListScript.SelectedBuilding = SelectedBuilding;
    }
}
