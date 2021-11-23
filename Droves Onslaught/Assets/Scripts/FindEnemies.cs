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

    private Arrays ListScript;
    private ButtonInfo BuildingInfo;


    //So if the building is spawned in the editor (I spawn it, not the player)
    [SerializeField] bool SpawnedViaEditor = false;
    public bool SpawnVisual = true;

    private bool RunOnce = false;


    private void Start()
    {
        MoveScript = GetComponent<MoveTowards>();
        Manager = GameObject.FindGameObjectWithTag("Manager");
        ListScript = Manager.GetComponent<Arrays>();
        BuildingInfo = gameObject.GetComponent<ButtonInfo>();

        if (SpawnedViaEditor)
        {

            PlacingBuildings PlaceBuilding = Manager.GetComponent<PlacingBuildings>();

            //Add building to list (of all buildings)
            ListScript.BuildingsList.Add(gameObject);

            
            //Add building to it's individual list in the dictionary
            string Name = BuildingInfo.Name;
            ListScript.BuildingDict[Name].Add(gameObject);
            

            if (SpawnVisual)
            {
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
            }
        }



        //Have this code here to run once (for the market buildings)
        //Having trouble setting their target to null because this script sets it to something else immediately
        if (MoveScript.Target == null && TargetList.Count > 0)
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

        //Every building re does line renderer
        Manager.GetComponent<PlacingBuildings>().SettingLineRenderers();
    }



    void Update()
    {
        //Have this here becasue the dictionary sets in awake, buildings add themselves to it in start
        //And then we set the target list from dictionary in update (otherwise targetlists would be half full due to how scripts execute)
        if (!RunOnce)
        {
            SetTargetList();
            RunOnce = true;
        }

        if (MoveScript.Target == null && !IsMarket)
        {
            SetTargetList();

            if(TargetList.Count > 0)
            {
                SetTargetAndLR();
            }
        }
    }


    //https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html 
    public GameObject FindClosestEnemy()
    {
        SetTargetList();


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

        if(closest != null)
        {
            closest.GetComponent<MoveTowards>().Shooter = gameObject;
        }

        return closest;
    }

    private void SetTargetAndLR()
    {
        //Put this into a function, so the if() won't ignore the Set LR section (because the target is no longer null)

        MoveScript.Target = FindClosestEnemy();


        //If gameobject is a producer:
        if (GetComponent<Producer>() != null)
        {
            //Need to call this after FindClosestEnemy() or else SettingLineRenderers() won't work
            Manager.GetComponent<PlacingBuildings>().SettingLineRenderers();
        }
    }


    private void SetTargetList()
    {
        /*string Name = BuildingInfo.Name;

        if (!ListScript.BuildingDict.ContainsKey(Name))
        {
            ListScript.BuildingDict[Name].Add(gameObject);
        }*/

        /*

        //For every target name (the list of gameobject names to target)
        for (int i = 0; i < TargetName.Count; i++)
        {
            //GameObject based off TargetName in the Dictionary
            foreach (GameObject go in Manager.GetComponent<Arrays>().BuildingDict[TargetName[i]])
            {
                //If the gameobject is not in the TargetList
                if (!TargetList.Contains(go))
                {
                    //Add to list
                    TargetList.Add(go); //When building is destroyed it's not removed, it should be
                }
            }
        }*/

        TargetList.Clear();
        for (int i = 0; i < TargetName.Count; i++)
        {
            TargetList.AddRange(ListScript.BuildingDict[TargetName[i]]);
        }
    }
}
