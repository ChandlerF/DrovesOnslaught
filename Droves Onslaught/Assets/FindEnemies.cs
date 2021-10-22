using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemies : MonoBehaviour
{

    private MoveTowards RotateScript;

    [SerializeField] string Tag;

    public float MaxRange = 100f;

    private void Start()
    {
        RotateScript = GetComponent<MoveTowards>();
    }

    void Update()
    {
        if(RotateScript.Target == null && GameObject.FindGameObjectsWithTag(Tag).Length > 0)
        {
            RotateScript.Target = FindClosestEnemy(MaxRange);
        }
    }



    //https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html 
    public GameObject FindClosestEnemy(float MaxRng)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(Tag);

        GameObject closest = null;

        float distance = Mathf.Infinity;

        Vector3 position = transform.position;


        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            Debug.Log(curDistance);
            Debug.Log(distance);
            Debug.Log(MaxRng);
            //Problem where MaxRng is always set to 5 no matter what, causing it so buildings can only coonect if super close
            //Also this function is being repeatedly called, losing fps
            //Tried connecting it with PlacingBuildings script but no
            if (curDistance < distance)
            {
                distance = curDistance;

                if (distance < MaxRng)
                {
                    closest = go;
                }
            }
        }
        return closest;
    }
}
