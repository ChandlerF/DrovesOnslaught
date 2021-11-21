using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemies : MonoBehaviour
{

    private MoveTowards MoveScript;

    public List<string> TargetName = new List<string>();

    public List<GameObject> TargetList = new List<GameObject>();

    private GameObject Manager;

    public float MaxRange = 100f;

    private bool IsMarket = false;

    //So if the building is spawned in the editor (I spawn it, not the player)
    [SerializeField] bool SpawnedViaEditor = false;

    private void Start()
    {
        MoveScript = GetComponent<MoveTowards>();
        Manager = GameObject.FindGameObjectWithTag("Manager");

        //Have this code here to run once (for the market buildings)
        //Having trouble setting their target to null because this script sets it to something else immediately
        if (MoveScript.Target == null && TargetsAlive())
        {
            MoveScript.Target = FindClosestEnemy();
        }

        else if (GetComponent<Producer>())
        {
            if (GetComponent<Producer>().IsMarket)
            {
                IsMarket = true;
            }
        }




        if (SpawnedViaEditor)
        {
            Arrays ListScript = Manager.GetComponent<Arrays>();
            PlacingBuildings PlaceBuilding = Manager.GetComponent<PlacingBuildings>();
            ButtonInfo BuildingInfo = gameObject.GetComponent<ButtonInfo>();

            //Add building to list (of all buildings)
            ListScript.BuildingsList.Add(gameObject);
            //Add building to it's individual list in the dictionary
            string Name = BuildingInfo.Name;
            ListScript.BuildingDict[Name].Add(gameObject);  


            //Reference the visual gameobject
            GameObject Visual = PlaceBuilding.Visual;
            //Spawn visual
            GameObject SpawnedVisual = Instantiate(Visual, transform.position, gameObject.transform.rotation);

            //Set building's visual
            BuildingInfo.RangeVisual = SpawnedVisual;

            //Find size to scale visual up to
            float Scale = Mathf.Sqrt(gameObject.GetComponent<FindEnemies>().MaxRange) * 2;
            //Set visual scale
            SpawnedVisual.transform.localScale = new Vector3(Scale, Scale, Scale);
            //Building pos = Visual pos
            gameObject.transform.position = SpawnedVisual.transform.position;



            //Add visual to list
            ListScript.VisualsList.Add(SpawnedVisual);


            //Every building re does line renderer
            PlaceBuilding.SettingLineRenderers();
        }
    }

    void Update()
    {
        if (MoveScript.Target == null && TargetsAlive() && !IsMarket)
        {
            MoveScript.Target = FindClosestEnemy();
        }
    }


    //https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html 
    public GameObject FindClosestEnemy()
    {
        //For every name that's a target
        for (int i = 0; i < TargetName.Count; i++)
        {
            //GameObject based off TargetName in the Dictionary
            foreach(GameObject go in Manager.GetComponent<Arrays>().BuildingDict[TargetName[i]])
            {
                //If the gameobject is not in the TargetList
                if (!TargetList.Contains(go))
                {
                    //Add to list
                    TargetList.Add(go); //When building is destroyed it's not removed, it should be
                }
            }
        }


        GameObject closest = null;

        float distance = Mathf.Infinity;

        Vector3 position = transform.position;


        foreach (GameObject go in TargetList)
        {
            if(go != null)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;


                if (curDistance < distance)
                {
                    distance = curDistance;

                    if (curDistance < MaxRange)
                    {
                        closest = go;
                    }
                }
            }
        }

    return closest;
    }





    //Have to put this in a function because the for loop
    private bool TargetsAlive()
    {
        //For each name in Target Name
        for (int i = 0; i < TargetName.Count; i++)
        {
            //If any gameobjects in lists, return 
            if(Manager.GetComponent<Arrays>().BuildingDict[TargetName[i]].Count > 0)
            {
                return true;
            }
            
        }


        return false;
    }
}
