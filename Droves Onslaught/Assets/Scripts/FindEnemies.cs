using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemies : MonoBehaviour
{

    private MoveTowards RotateScript;

    [SerializeField] string TagOne;
    [SerializeField] string TagTwo = null;

    private bool TagTwoIsNull = true;

    public float MaxRange = 100f;

    private void Start()
    {
        RotateScript = GetComponent<MoveTowards>();

        if(string.IsNullOrEmpty(TagTwo))
        {
            TagTwo = TagOne;
            TagTwoIsNull = true;
        }
        else
        {
            TagTwoIsNull = false;
        }
    }

    void Update()
    {
        if (RotateScript.Target == null && NumBuildWithTag() > 0)
        {
            RotateScript.Target = FindClosestEnemy();
        }
    }

    private int NumBuildWithTag()
    {
        int x = 0;

        if (!TagTwoIsNull)
        {
            x = GameObject.FindGameObjectsWithTag(TagOne).Length + GameObject.FindGameObjectsWithTag(TagTwo).Length;
        }
        else
        {
            x = GameObject.FindGameObjectsWithTag(TagOne).Length;
        }
        return x;
    }

    //https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html 
    public GameObject FindClosestEnemy()
    {
        List<GameObject> gos = new List<GameObject>();
        gos.AddRange(GameObject.FindGameObjectsWithTag(TagOne));
        if(!TagTwoIsNull)
        {
            gos.AddRange(GameObject.FindGameObjectsWithTag(TagTwo));
        }


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
