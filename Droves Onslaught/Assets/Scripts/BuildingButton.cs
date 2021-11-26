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
        //If another building is not selected and you're not trying to place down a building
        //tl;dr spawn buttons canvas if nothing else selected
        if (!ListScript.UpgradeButtonActive && !ListScript.InPlacingBuildingMode)
        {
            //Disable Base Buttons (the shop to place the original 3 buildings)
            ListScript.ChangeButtonsActive();

            //Get Building
            ButtonInfo ButtonScript = SelectedBuilding.GetComponent<ButtonInfo>();

            if (ButtonScript.RangeVisual != null)
            {
                //Enable building visual
                ButtonScript.RangeVisual.GetComponent<SpriteRenderer>().enabled = true;
            }

            //Spawn Canvas (with buttons)
            GameObject SpawnedCanvas = Instantiate(Buttons, transform.position, transform.rotation);
            //Sets Manager's active canvas
            Manager.GetComponent<PlacingBuildings>().ActiveUpgradeCanvas = SpawnedCanvas;


            ButtonParent = SpawnedCanvas.transform.GetChild(0).gameObject;


            if (ButtonScript.BuildingUpgrades.Count > 0)
            {


                //Set's upgrade buttons for buildings that have upgrades
                //Have several buttons not active and per upgrade available set active appropriately

                    for(int i = 0; i < ButtonScript.BuildingUpgrades.Count; i++)
                    {
                        GameObject Button = ButtonParent.transform.GetChild(0).GetChild(i).gameObject;
                    
                        //Turn button on
                        Button.SetActive(true);

                        //Set SetButton Building
                        Button.GetComponent<SetButton>().Building = ButtonScript.BuildingUpgrades[i];
                        //Set DestroyParent's SelectedBuilding
                        Button.gameObject.GetComponent<DestroyParent>().SelectedBuilding = SelectedBuilding; ;
                    }
            }
            
            
            

            //Sets the destroy Button on upgrade screen
            ButtonParent.transform.GetChild(1).GetChild(0).gameObject.GetComponent<DestroyParent>().SelectedBuilding = SelectedBuilding;
            //Sets hide buttons button
            ButtonParent.transform.GetChild(1).GetChild(1).gameObject.GetComponent<DestroyParent>().SelectedBuilding = SelectedBuilding;
            //Sets Tether buttons button
            ButtonParent.transform.GetChild(1).GetChild(2).gameObject.GetComponent<DestroyParent>().SelectedBuilding = SelectedBuilding;



            ListScript.UpgradeButtonActive = true;
        }

        //if in tether mode and the new building is in old buildings target list
        else if (ListScript.InTetherMode && ListScript.SelectedBuilding.GetComponent<FindEnemies>().TargetName.Contains(SelectedBuilding.name.Remove(SelectedBuilding.name.Length - 7)))   //called from new building when clicked
        {
            SetTetherModeFalse();
        }

        //If in tether mode and clicks on self, target = null
        else if(ListScript.InTetherMode && ListScript.SelectedBuilding == SelectedBuilding)
        {
            //Set 'old' building target to null
            ListScript.SelectedBuilding.GetComponent<MoveTowards>().Target = null; //Cant do gameObject, do SelectedBuilding (The new building)
            Debug.Log(ListScript.SelectedBuilding.GetComponent<MoveTowards>().Target);

            //Call Liner Renderer
            Manager.GetComponent<PlacingBuildings>().SettingLineRenderers();

            //Destroys Upgrade Canvas after tethering - Calls Destroy Parent
            Manager.GetComponent<PlacingBuildings>().ActiveUpgradeCanvas.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<DestroyParent>().DestroyTheParent();


            //Set tether mode false
            ListScript.InTetherMode = false;
        }
    }
    
    
    
    //changes which building it's tethered to
    public void SetTetherModeFalse()
    {
        //Set 'old' building target to new (this gameobject) from ListScript (Array.cs)
        ListScript.SelectedBuilding.GetComponent<MoveTowards>().Target = SelectedBuilding; //Cant do gameObject, do SelectedBuilding (The new building)

        
        //Call Liner Renderer
        Manager.GetComponent<PlacingBuildings>().SettingLineRenderers();

        //Destroys Upgrade Canvas after tethering - Calls Destroy Parent
        Manager.GetComponent<PlacingBuildings>().ActiveUpgradeCanvas.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<DestroyParent>().DestroyTheParent();


        //Set tether mode false
        ListScript.InTetherMode = false;
    }
}
