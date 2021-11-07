using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrays : MonoBehaviour
{
    public List<GameObject> BuildingsList = new List<GameObject>();

    public List<GameObject> VisualsList = new List<GameObject>();

    [SerializeField] GameObject BaseButtonsParent;

    [SerializeField] List<string> DictNames = new List<string>();

    public Dictionary<string, List<GameObject>> BuildingDict = new Dictionary<string, List<GameObject>>();

    public bool UpgradeButtonActive = false;


    private void Start()
    {
        //Can't make lists in the dictionary from the inspector, so I have a list of names and makes the dict lists here
        for (int i = 0; i < DictNames.Count; i++)    
        {
            BuildingDict.Add(DictNames[i], new List<GameObject>());
        }
    }

    /*private void Update()
    {
        for (int i = 0; i < DictNames.Count; i++)
        {
            Debug.Log(BuildingDict[DictNames[i]]);
        }
    }*/
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
