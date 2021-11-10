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


    private void Start()
    {
        MoveScript = GetComponent<MoveTowards>();
        Manager = GameObject.FindGameObjectWithTag("Manager");
    }

    void Update()
    {
        if (MoveScript.Target == null && TargetsAlive())
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
