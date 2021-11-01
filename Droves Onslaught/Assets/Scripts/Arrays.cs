using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrays : MonoBehaviour
{
    public List<GameObject> BuildingsList = new List<GameObject>();

    public List<GameObject> VisualsList = new List<GameObject>();

    [SerializeField] GameObject BaseButtonsParent;


    public void ChangeButtonsActive()
    {
        if(BaseButtonsParent.activeSelf == true)
        {
            BaseButtonsParent.SetActive(false);
        }
        else
        {
            BaseButtonsParent.SetActive(true);
        }
    }
}
