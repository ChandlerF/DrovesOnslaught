using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButton : MonoBehaviour
{
    [SerializeField] GameObject Buttons;

    private Arrays ListScript;

    public GameObject SelectedBuilding;

    private GameObject Manager;

    private GameObject ButtonParent;

    private void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("Manager");
        ListScript = Manager.GetComponent<Arrays>();
        SelectedBuilding = transform.parent.parent.gameObject;
    }


    public void SpawnButtons()  //Clicked on building
    {
            //Set ListScript Selected, to reference globally
            ListScript.SelectedBuilding = SelectedBuilding;
            
            
        if (!ListScript.UpgradeButtonActive && !ListScript.InPlacingBuildingMode)
        {
            //Disable Base Buttons (the shop to place the original 3 buildings)
            ListScript.ChangeButtonsActive();

            //Get Building
            ButtonInfo ButtonScript = SelectedBuilding.GetComponent<ButtonInfo>();

            //Enable building visual
            SelectedBuilding.GetComponent<ButtonInfo>().RangeVisual.GetComponent<SpriteRenderer>().enabled = true;

             //Spawn Canvas (with buttons)
             GameObject SpawnedCanvas = Instantiate(Buttons, transform.position, transform.rotation);
                
            if (ButtonScript.BuildingUpgrades.Count > 0)
            {
                ButtonParent = SpawnedCanvas.transform.GetChild(0).gameObject;


                //Set's upgrade buttons for buildings that have upgrades
                //Have several buttons not active and per upgrade available set active appropriately

                    for(int i = 0; i < ButtonScript.BuildingUpgrades.Count; i++)
                    {
                        GameObject Button = ButtonParent.transform.GetChild(i).gameObject;
                    
                        //Turn button on
                        Button.SetActive(true);

                        //Set SetButton Building
                        Button.GetComponent<SetButton>().Building = ButtonScript.BuildingUpgrades[i];
                        //Set DestroyParent's SelectedBuilding
                        Button.gameObject.GetComponent<DestroyParent>().SelectedBuilding = SelectedBuilding; ;
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
                ButtonParent.transform.GetChild(3).gameObject.GetComponent<DestroyParent>().SelectedBuilding = SelectedBuilding;
                //Sets hide buttons button
                ButtonParent.transform.GetChild(4).gameObject.GetComponent<DestroyParent>().SelectedBuilding = SelectedBuilding;
                
                ListScript.UpgradeButtonActive = true;
                
                
        }
        else if (ListScript.InTetherMode)   //If new building is selected while in tether mode:
        {
            SetTetherModeFalse();
        }
    }
    
    
    
    public void SetTetherModeFalse()
    {
        //Set tether mode false
        ListScript.InTetherMode = false;
        //Set Selected Building's Target to the New Taget from List Script (Array.cs)
        ListScript.SelectedBuilding.GetComponent<MoveTowards>().Target = SelectedBuilding; //Cant do gameObject, do SelectedBuilding (The new building)
        
        Debug.Log("Set new building's target to current");
        Debug.Log("New Target:" + ListScript.SelectedBuilding.GetComponent<MoveTowards>().Target);
        Debug.Log("Current: " + SelectedBuilding);
        
        //Call Liner Renderer
        Manager.GetComponent<PlacingBuildings>().SettingLineRenderers();
    }
}
