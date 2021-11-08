using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButton : MonoBehaviour
{
    [SerializeField] GameObject Buttons;

    private Arrays ListScript;

    public GameObject SelectedBuilding;

    private GameObject Manager;
    private void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("Manager");
        ListScript = Manager.GetComponent<Arrays>();
        SelectedBuilding = transform.parent.parent.gameObject;
    }


    public void SpawnButtons()  //Clicked on building
    {
        if (!ListScript.UpgradeButtonActive)
        {
            //Set ListScript Selected, to reference globally
            ListScript.SelectedBuilding = SelectedBuilding;

            //DisableOther Buttons
            ListScript.ChangeButtonsActive();

            //GetBuilding
            ButtonInfo ButtonScript = SelectedBuilding.GetComponent<ButtonInfo>();

            //Enable building visual
            SelectedBuilding.GetComponent<ButtonInfo>().RangeVisual.GetComponent<SpriteRenderer>().enabled = true;

             //Spawn Canvas (with buttons)
             GameObject SpawnedCanvas = Instantiate(Buttons, transform.position, transform.rotation);
                
            if (ButtonScript.BuildingUpgrades.Count > 0)
            {
                GameObject ButtonParent = SpawnedCanvas.transform.GetChild(0).gameObject;
                //Set Buttons Reference
                GameObject ButtonOne = ButtonParent.transform.GetChild(0).gameObject;
                GameObject ButtonTwo = ButtonParent.transform.GetChild(1).gameObject;


                int i = 0;
                //Set's upgrade buttons for buildings that have upgrades
                //Have several buttons not active and per upgrade available set active appropriately
                foreach(GameObject Button in ButtonScript.BuildingUpgrades)
                {
                    //Set each button to on
                    Button.SetActive() = true;
                    //SetButton Building
                    Button.GetComponent<SetButton>().Building = ButtonScript.BuildingUpgrades[i];
                    //Destroy SelectedBuilding
                    Button.GetComponent<DestroyParent>().SelectedBuilding = SelectedBuilding;;
                    
                    i++;
                }


                /*
                //Change button's building reference
                ButtonOne.GetComponent<SetButton>().Building = ButtonScript.BuildingUpgrades[0];

                ButtonTwo.GetComponent<SetButton>().Building = ButtonScript.BuildingUpgrades[1];

                //Change button's SelectedBuilding reference
                ButtonOne.GetComponent<DestroyParent>().SelectedBuilding = SelectedBuilding;
                ButtonTwo.GetComponent<DestroyParent>().SelectedBuilding = SelectedBuilding;
                */
            }
            
            
            

                //Sets the destroy Button on upgrade screen
                ButtonParent.transform.GetChild(2).gameObject.GetComponent<DestroyParent>().SelectedBuilding = SelectedBuilding;
                //Sets hide buttons button
                ButtonParent.transform.GetChild(3).gameObject.GetComponent<DestroyParent>().SelectedBuilding = SelectedBuilding;
                
                ListScript.UpgradeButtonActive = true;
                
                
        }
        else if (ListScript.InTetherMode)   //If new building is selected while in tether mode:
        {
            ListScript.SelectedBuilding.GetComponent<MoveTowards>().Target = SelectedBuilding; //Cant do gameObject, do SelectedBuilding (The new building)
            Manager.GetComponent<PlacingBuildings>().SettingLineRenderers();
        }
    }
}
