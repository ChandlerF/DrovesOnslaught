using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingBuildings : MonoBehaviour
{
    [SerializeField] GameObject BuildingChecker;

    public GameObject Visual;

    private Arrays ListScript;

    [SerializeField] GameObject Tower;

    private void Start()
    {
        ListScript = GetComponent<Arrays>();

        //Spawn Tower//
        float x = Random.Range(-14, -7);
        float y = Random.Range(1, -5);

        Vector2 SpawnPos = new Vector2(x, y);
        Instantiate(Tower, SpawnPos, Quaternion.identity);
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
        gos.AddRange(ListScript.BuildingDict["Miner"]);
        gos.AddRange(ListScript.BuildingDict["Factory"]);


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
