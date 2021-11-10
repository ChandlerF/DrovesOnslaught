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
            //Sets Manager's active canvas
            Manager.GetComponent<PlacingBuildings>().ActiveUpgradeCanvas = SpawnedCanvas;


            ButtonParent = SpawnedCanvas.transform.GetChild(0).gameObject;


            if (ButtonScript.BuildingUpgrades.Count > 0)
            {


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
            }
            
            
            

            //Sets the destroy Button on upgrade screen
            ButtonParent.transform.GetChild(3).gameObject.GetComponent<DestroyParent>().SelectedBuilding = SelectedBuilding;
            //Sets hide buttons button
            ButtonParent.transform.GetChild(4).gameObject.GetComponent<DestroyParent>().SelectedBuilding = SelectedBuilding;
            //Sets Tether buttons button
            ButtonParent.transform.GetChild(5).gameObject.GetComponent<DestroyParent>().SelectedBuilding = SelectedBuilding;



            ListScript.UpgradeButtonActive = true;
        }


        else if (ListScript.InTetherMode && ListScript.SelectedBuilding.GetComponent<FindEnemies>().TargetName.Contains(SelectedBuilding.name.Remove(SelectedBuilding.name.Length - 7)))   //called from new building when clicked
        {
            SetTetherModeFalse();
        }
    }
    
    
    
    public void SetTetherModeFalse()
    {
        //Set 'old' building to new (this gameobject) from ListScript (Array.cs)
        ListScript.SelectedBuilding.GetComponent<MoveTowards>().Target = SelectedBuilding; //Cant do gameObject, do SelectedBuilding (The new building)

        
        //Call Liner Renderer
        Manager.GetComponent<PlacingBuildings>().SettingLineRenderers();

        //Destroys Upgrade Canvas after tethering - Calls Destroy Parent
        Manager.GetComponent<PlacingBuildings>().ActiveUpgradeCanvas.transform.GetChild(0).transform.GetChild(4).gameObject.GetComponent<DestroyParent>().DestroyTheParent();


        //Set tether mode false
        ListScript.InTetherMode = false;
    }
}
