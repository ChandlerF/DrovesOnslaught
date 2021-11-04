using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingBuildings : MonoBehaviour
{
    [SerializeField] GameObject BuildingChecker;

    public GameObject Visual;

    private Arrays ListScript;

    private void Start()
    {
        ListScript = GetComponent<Arrays>();
    }





    public void ClickedButton(GameObject Building)
    {
        Vector3 mousePos = Input.mousePosition;
        //mousePos.z = Camera.main.nearClipPlane; //put 30
        Vector3 worldPosition = new Vector3(Camera.main.ScreenToWorldPoint(mousePos).x, Camera.main.ScreenToWorldPoint(mousePos).y, 0);


        GameObject SpawnedCheker = Instantiate(BuildingChecker, worldPosition, BuildingChecker.transform.rotation);   ///////////////
        SpawnedCheker.GetComponent<BuildingChecker>().Building = Building.GetComponent<SetButton>().Building;

        SpawnedCheker.GetComponent<SpriteRenderer>().sprite = Building.GetComponent<SetButton>().Building.GetComponent<SpriteRenderer>().sprite;

        ListScript.ChangeButtonsActive();
    }








    public void SettingLineRenderers()  //For when a building is placed, it's line renderer and target has to be changed
    {
        List<GameObject> gos = new List<GameObject>();
        gos.AddRange(GameObject.FindGameObjectsWithTag("Producer"));
        gos.AddRange(GameObject.FindGameObjectsWithTag("Factory"));


        foreach (GameObject go in gos)
        {
            if(go.GetComponent<Producer>().TargetBuilding != null)
            {
                GameObject ClosestEnemy = go.GetComponent<FindEnemies>().FindClosestEnemy();

                go.GetComponent<Producer>().SetLRPos(ClosestEnemy);

                go.GetComponent<Producer>().TargetBuilding = ClosestEnemy;

                go.GetComponent<MoveTowards>().Target = ClosestEnemy;
            }
        }
    }
}
