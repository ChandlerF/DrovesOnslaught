using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableVisuals : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableAllVisuals(bool IsOn)
    {
        List<GameObject> VList = GameObject.FindGameObjectWithTag("Manager").GetComponent<Arrays>().VisualsList;


        foreach (GameObject go in VList)
        {
            go.SetActive(IsOn);
        }
    }
}
