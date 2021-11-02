using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButton : MonoBehaviour
{
    [SerializeField] GameObject Buttons;

    private Arrays ListScript;

    public GameObject SelectedBuilding;

    private void Start()
    {
        ListScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Arrays>();
        SelectedBuilding = transform.parent.parent.gameObject;
    }


    public void SpawnButtons()
    {
        //DisableOther Buttons
        ListScript.ChangeButtonsActive();

        //GetBuilding
        ButtonInfo ButtonScript = SelectedBuilding.GetComponent<ButtonInfo>();


        if(ButtonScript.BuildingUpgrades.Count > 0)
        {
            //Spawn Canvas (with buttons)
            GameObject SpawnedCanvas = Instantiate(Buttons, transform.position, transform.rotation);

            GameObject ButtonParent = SpawnedCanvas.transform.GetChild(0).gameObject;
            //Set Buttons Reference
            GameObject ButtonOne = ButtonParent.transform.GetChild(0).gameObject;
            GameObject ButtonTwo = ButtonParent.transform.GetChild(1).gameObject;

            //Change button's building reference
            ButtonOne.GetComponent<SetButton>().Building = ButtonScript.BuildingUpgrades[0];

            ButtonTwo.GetComponent<SetButton>().Building = ButtonScript.BuildingUpgrades[1];

            //Change button's SelectedBuilding reference
            ButtonOne.GetComponent<DestroyParent>().SelectedBuilding = SelectedBuilding;
            ButtonTwo.GetComponent<DestroyParent>().SelectedBuilding = SelectedBuilding;
        }
    }
}
