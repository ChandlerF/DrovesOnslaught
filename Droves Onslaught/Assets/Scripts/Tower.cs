using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int Score = 0;

    private GameObject Manager;

    private void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("Manager");

        Manager.GetComponent<Arrays>().BuildingDict["Tower"].Add(gameObject);
    }


    /*private void Update()
    {
        if(Score > PointsToWin)
    }*/
}
