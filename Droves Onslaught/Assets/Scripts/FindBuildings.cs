using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBuildings : MonoBehaviour
{

    private MoveTowards MoveScript;


    public Arrays ListScript;   //Set by enemy spawner

    private void Start()
    {
        MoveScript = GetComponent<MoveTowards>();

        GameObject.FindGameObjectWithTag("Manager").GetComponent<Arrays>().BuildingDict["Enemy"].Add(gameObject);
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
