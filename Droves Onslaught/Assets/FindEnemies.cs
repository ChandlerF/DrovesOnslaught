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
            RotateScript.Target = FindClosestEnemy();
        }
    }



    //https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html 
    public GameObject FindClosestEnemy()
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


            if (curDistance < distance)
            {
                distance = curDistance;

                if (curDistance < MaxRange)
                {
                    closest = go;
                }
            }
        }
        return closest;
    }
}
