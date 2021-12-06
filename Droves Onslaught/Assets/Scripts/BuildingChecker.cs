using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//need to put on start for buildings
//the same things that i do when I spawn it
public class BuildingChecker : MonoBehaviour
{
    public bool TouchingBuilding = false;

    public bool TouchingOre = false;

    public GameObject Building = null;

    private bool DestroyOres = false;

    private Arrays ListScript;

    [SerializeField] GameObject SpawnParticles;
    
    private SpriteRenderer SR;
    [SerializeField] Color CanPlace;
    [SerializeField] Color CannotPlace;
    
    

    private void Start()
    {
     SR = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 30;    // it's this 30 + the camera's -30
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);


        transform.position = worldPosition;

        //https://answers.unity.com/questions/704939/touch-input-certain-screen-part.html       Quad
        //https://answers.unity.com/questions/1096020/split-screen-in-half-touch-controls.html      Rect
        //Could use finger Position and draw a ray to see if it hits an object
        //Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        //GIF :         -       http://pastebin.com/eqFUqgtF
        //GIF creator:  https://assetstore.unity.com/packages/tools/utilities/gif-creator-42302
        //Drag button   https://answers.unity.com/questions/350220/how-do-i-drag-a-guibutton-.html
        //Drag from button      https://answers.unity.com/questions/706042/how-can-i-spawn-and-drag-from-a-button-click.html
       
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)// && Input.GetTouch(0).position.y < Screen.height * 0.3 //Releases touch on bottom of screen
        {
            CallSpawnBuilding();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            CallSpawnBuilding();
        }


        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Canceled)
        {
            Cancel();
        }
        else if (Input.GetMouseButtonDown(1)) 
        {
            Cancel();
        }

        if(Time.timeScale == 0)
        {
            Cancel();
        }
    }

    private void Cancel()
    {
        Arrays ListScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Arrays>();
        ListScript.ChangeButtonsActive();

        ListScript.InPlacingBuildingMode = false;
        Destroy(gameObject);
    }


    private void CallSpawnBuilding()
    {
        if (!TouchingBuilding && Time.timeScale != 0)
        {
            if (Building.CompareTag("Producer"))
            {
                if (TouchingOre)
                {
                    DestroyOres = true;

                    SpawnBuilding();
                }
            }

            else
            {
                if (!TouchingOre)
                {
                    SpawnBuilding();

                    ListScript.InPlacingBuildingMode = false;
                    Destroy(gameObject);
                }
            }
        }
    }


    private void SpawnBuilding()
    {
        GameObject Manager = GameObject.FindGameObjectWithTag("Manager");
        ListScript = Manager.GetComponent<Arrays>();
        PlacingBuildings PlaceBuilding = Manager.GetComponent<PlacingBuildings>();

        Vector3 SpawnPos = new Vector3(transform.position.x, transform.position.y, 0);

        //Spawn building
        GameObject SpawnedBuilding = Instantiate(Building, SpawnPos, Building.transform.rotation);
        //Add building to list (of all buildings)
        ListScript.BuildingsList.Add(SpawnedBuilding);
        //Add building to it's individual list in the dictionary
        ListScript.BuildingDict[SpawnedBuilding.GetComponent<ButtonInfo>().Name].Add(SpawnedBuilding);

        ButtonInfo BuildingInfo = SpawnedBuilding.GetComponent<ButtonInfo>();

        if (SpawnedBuilding.GetComponent<FindEnemies>().SpawnVisual)
        {
            //Reference the visual gameobject
            GameObject Visual = PlaceBuilding.Visual;
            //Spawn visual
            GameObject SpawnedVisual = Instantiate(Visual, transform.position, Building.transform.rotation);

            //Set building's visual
            BuildingInfo.RangeVisual = SpawnedVisual;

            //Find size to scale visual up to
            float Scale = Mathf.Sqrt(SpawnedBuilding.GetComponent<FindEnemies>().MaxRange) * 2;
            //Set visual scale
            SpawnedVisual.transform.localScale = new Vector3(Scale, Scale, Scale);
            //Building pos = Visual pos
            SpawnedBuilding.transform.position = SpawnedVisual.transform.position;



            //Add visual to list
            ListScript.VisualsList.Add(SpawnedVisual);

            ListScript.PlayerHasPlacedBuilding = true;
        }

        


        //Every building re does line renderer
        PlaceBuilding.SettingLineRenderers();
        //Take from player scrap
        Manager.GetComponent<Player>().Scrap -= BuildingInfo.Cost;

        //Manager.GetComponent<Player>().CameraShake(0.1f);

        Manager.GetComponent<Player>().CameraShake(0.1f);
        Instantiate(SpawnParticles, SpawnedBuilding.transform.position, SpawnParticles.transform.rotation);

        //Turn buttons on / off
        ListScript.ChangeButtonsActive();
    }


    private void OnTriggerStay2D(Collider2D col)        //Called Every frame of collision
    {
        if (col.gameObject.layer == 6)       //Buildings
        {
            //Debug.Log("Touching Building");
            
            TouchingBuilding = true;
            
            SR.color = CannotPlace;
        }
        else if (col.gameObject.layer == 10)        //Ore
        {
            //Debug.Log("Touching Ore");
            
            TouchingOre = true;
            
            if(Building.CompareTag("Producer"))
            {
             SR.color = CanPlace;
            }
            else
            {
             SR.color = CannotPlace;
            }
        }

        if (DestroyOres)        //Called when Building is placed, destroys ore's then the building checker
        {
            if (col.gameObject.layer == 10)
            {
                Destroy(col.gameObject);
            }
            
            ListScript.InPlacingBuildingMode = false;
            Destroy(gameObject);
        }
    }




    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 6)       //Buildings
        {
            TouchingBuilding = false;
            
            SR.color = CanPlace;
            //Debug.Log("Not Touching Building");
        }
        else if (col.gameObject.layer == 10)        //Ore
        {
           // Debug.Log("Not Touching Ore");
           
            TouchingOre = false;
            
            if(!Building.CompareTag("Producer"))
            {
             SR.color = CanPlace;
            }
            else
            {
             SR.color = CannotPlace;
            }
        }
    }
}
