using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButton : MonoBehaviour
{
    [SerializeField] GameObject Buttons;

    private Arrays ListScript;

    private void Start()
    {
        ListScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Arrays>();
    }


    public void SpawnButtons()
    {
        //DisableOther Buttons
        ListScript.ChangeButtonsActive();

        //GetBuilding
        ButtonInfo ButtonScript = transform.parent.parent.GetComponent<ButtonInfo>();

        //If list.count == 0 then don't show

        if(ButtonScript.BuildingUpgrades.Count > 0)
        {
            GameObject SpawnedCanvas = Instantiate(Buttons, transform.position, transform.rotation);

            GameObject ButtonParent = SpawnedCanvas.transform.GetChild(0).gameObject;

            GameObject ButtonOne = ButtonParent.transform.GetChild(0).gameObject;
            GameObject ButtonTwo = ButtonParent.transform.GetChild(1).gameObject;

            ButtonOne.GetComponent<SetButton>().Building = ButtonScript.BuildingUpgrades[0];

            ButtonTwo.GetComponent<SetButton>().Building = ButtonScript.BuildingUpgrades[1];

        }

        //Youd made the buttons into a prefab
        //Spawn in a button, set it's gameobject to be parent
        //Make it's position be (x, y)

        //Have those buttons: 
        //Destroy building
        //Spawn new building in the same spot
        //And maybe reset line renderes if you make them manual

        //transform.parent.parent.GetComponent<BuildingUpgrades>().Buildings
    }
}
