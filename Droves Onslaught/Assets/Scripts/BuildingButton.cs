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
        GameObject Manager = GameObject.FindGameObjectWithTag("Manager");
        ListScript = Manager.GetComponent<Arrays>();
        SelectedBuilding = transform.parent.parent.gameObject;
    }


    public void SpawnButtons()  //Clicked on building
    {
        if (!ListScript.UpgradeButtonActive)
        {
            //DisableOther Buttons
            ListScript.ChangeButtonsActive();

            //GetBuilding
            ButtonInfo ButtonScript = SelectedBuilding.GetComponent<ButtonInfo>();

            //Enable building visual
            SelectedBuilding.GetComponent<ButtonInfo>().RangeVisual.GetComponent<SpriteRenderer>().enabled = true;


            if (ButtonScript.BuildingUpgrades.Count > 0)
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

                //Sets the destroy Button on upgrade screen
                ButtonParent.transform.GetChild(2).gameObject.GetComponent<DestroyParent>().SelectedBuilding = SelectedBuilding;
                //Sets hide buttons button
                ButtonParent.transform.GetChild(3).gameObject.GetComponent<DestroyParent>().SelectedBuilding = SelectedBuilding;

                ListScript.UpgradeButtonActive = true;
            }
        }
    }
}
