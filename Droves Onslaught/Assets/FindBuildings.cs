using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBuildings : MonoBehaviour
{

    private MoveTowards MoveScript;

    [SerializeField] string TagOne;
    [SerializeField] string TagTwo;
    [SerializeField] string TagThree;

    private void Start()
    {
        MoveScript = GetComponent<MoveTowards>();
    }

    void Update()
    {
        if (MoveScript.Target == null)
        {
            if (GameObject.FindGameObjectsWithTag(TagOne).Length > 0 || GameObject.FindGameObjectsWithTag(TagTwo).Length > 0 || GameObject.FindGameObjectsWithTag(TagThree).Length > 0)
            {
                MoveScript.Target = FindClosestEnemy();
            }
        }
    }



    //https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html 
    public GameObject FindClosestEnemy()
    {
        List<GameObject> gos = GameObject.FindGameObjectWithTag("Manager").GetComponent<Arrays>().BuildingsList;


        GameObject closest = null;

        float distance = Mathf.Infinity;

        Vector3 position = transform.position;


        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }














    /*
    void Update()
    {
        //Set Target to move towards
        if(MoveScript.Target == null)
        {

            //Finds buildings, Priority: Weapon, Factory, and Producer

            if (GameObject.FindGameObjectsWithTag("Weapon").Length > 0)
            {
                MoveScript.Target = GameObject.FindGameObjectWithTag("Weapon");
            } 
            else if(GameObject.FindGameObjectsWithTag("Factory").Length > 0)
            {
                MoveScript.Target = GameObject.FindGameObjectWithTag("Factory");
            }
            else if(GameObject.FindGameObjectsWithTag("Producer").Length > 0)
            {
                MoveScript.Target = GameObject.FindGameObjectWithTag("Producer");
            }
        }
    }*/
}
