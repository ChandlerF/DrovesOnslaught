using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParent : MonoBehaviour
{
    private GameObject Manager;
    private Arrays ListScript;
    private PlacingBuildings PlaceBuildings;

    private void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("Manager");
        ListScript = Manager.GetComponent<Arrays>();
        PlaceBuildings = Manager.GetComponent<PlacingBuildings>();
    }

    public void DestroyTheParent()
    {
        ListScript.ChangeButtonsActive();

        Destroy(transform.parent.gameObject);
    }


    public void SpawnBuilding(GameObject go)
    {
        PlaceBuildings.ClickedButton(go);
    }
}
