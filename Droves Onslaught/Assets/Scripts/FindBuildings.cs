using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBuildings : MonoBehaviour
{

    private MoveTowards MoveScript;


    public Arrays ListScript;   //Set by enemy spawner



    private void Start()
    {
        string Name = GetComponent<ButtonInfo>().Name;
        MoveScript = GetComponent<MoveTowards>();


        //This is only for when enemies are spawned in via the editor, not from the enemy spawner
        if (!ListScript)
        {
            ListScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Arrays>();
        }

        ListScript.BuildingDict[Name].Add(gameObject);
    }

    void Update()
    {
        if (MoveScript.Target == null)
        {
            if (ListScript.BuildingsList.Count > 0)
            {
                MoveScript.Target = FindClosestEnemy(ListScript);
            }
        }
    }



    //https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html 
    public GameObject FindClosestEnemy(Arrays ListScript)
    {
        GameObject closest = null;

        float distance = Mathf.Infinity;

        Vector3 position = transform.position;


        foreach (GameObject go in ListScript.BuildingsList)
        {
            if(go != null)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
        }
        return closest;
    }
}
