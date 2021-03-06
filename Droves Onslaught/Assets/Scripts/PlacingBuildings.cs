using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingBuildings : MonoBehaviour
{
    [SerializeField] GameObject BuildingChecker;

    public GameObject Visual;

    private Arrays ListScript;

    [SerializeField] GameObject Tower;

    //Global so when tether is manually set, the upgrade menu can be destroyed
    public GameObject ActiveUpgradeCanvas;

    [SerializeField] bool SpawnTower = false;

    private void Awake()
    {
        ListScript = GetComponent<Arrays>();

        //Spawn Tower//
        if (SpawnTower)
        {
            float x = Random.Range(-14, -7);
            float y = Random.Range(1, -5);

            Vector2 SpawnPos = new Vector2(x, y);
            GameObject SpawnedTower = Instantiate(Tower, SpawnPos, Quaternion.identity);
            ListScript.BuildingsList.Add(SpawnedTower);
        }
    }





    public void ClickedButton(GameObject Building)
    {
        Vector3 mousePos = Input.mousePosition;
        //mousePos.z = Camera.main.nearClipPlane; //put 30
        Vector3 worldPosition = new Vector3(Camera.main.ScreenToWorldPoint(mousePos).x, Camera.main.ScreenToWorldPoint(mousePos).y, 0);


        GameObject SpawnedCheker = Instantiate(BuildingChecker, worldPosition, BuildingChecker.transform.rotation);   ///////////////
        SpawnedCheker.GetComponent<BuildingChecker>().Building = Building.GetComponent<SetButton>().Building;

        SpawnedCheker.GetComponent<SpriteRenderer>().sprite = Building.GetComponent<SetButton>().Building.GetComponent<SpriteRenderer>().sprite;

        ListScript.InPlacingBuildingMode = true;
        ListScript.ChangeButtonsActive();
    }








    public void SettingLineRenderers()  //For when a building is placed, it's line renderer and target has to be changed
    {
        //Setting list of all buildings that have line renderers (could maybe do ListScript.BuildingList and get every GameObject with a LineRenderer or producer script)
        //List of producers
        List<GameObject> gos = new List<GameObject>();

        gos.AddRange(ListScript.BuildingDict["Miner"]);
        gos.AddRange(ListScript.BuildingDict["Factory"]);
        gos.AddRange(ListScript.BuildingDict["Transport"]);


        foreach (GameObject go in gos)
        {
            //if building (producer) has target:
            if(go.GetComponent<MoveTowards>().Target != null)
            {
                GameObject ClosestEnemy = go.GetComponent<MoveTowards>().Target;

                go.GetComponent<Producer>().SetLRPos(ClosestEnemy);

                go.GetComponent<Producer>().TargetBuilding = ClosestEnemy;

               // go.GetComponent<MoveTowards>().Target = ClosestEnemy;     //Was commented out when manual tethering was added
            }
        }
    }
}
