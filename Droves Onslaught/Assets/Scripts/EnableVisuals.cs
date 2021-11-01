using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableVisuals : MonoBehaviour
{
    private GameObject Manager;
    private void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("Manager");
    }

    public void EnableAllVisuals(bool IsOn)
    {
        List<GameObject> VList = Manager.GetComponent<Arrays>().VisualsList;


        foreach (GameObject go in VList)
        {
            go.GetComponent<SpriteRenderer>().enabled = IsOn;
        }
    }
}
