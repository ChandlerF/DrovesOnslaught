using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int HP;

    [SerializeField] GameObject Particles;

    [SerializeField] int Scrap;
    
    [SerializeField] GameObject TextPopUp;
    
    private GameObject Manager;
    
    private Player PlayerScript;

    private void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("Manager");
        
        PlayerScript = Manager.GetComponent<Player>();
    }



    void Update()
    {
        if(HP <= 0)
        {
            Death();
        }
    }


    public void Damage(int Dmg)
    {
        HP -= Dmg;
    }


    private void Death()
    {
        string Name = GetComponent<ButtonInfo>().Name;
        MoveTowards MoveScript = GetComponent<MoveTowards>();

        //If on death, it's scrap is more than 0, add it
        if (Scrap > 0)
        {
            SpawnText(Scrap);
        }


        if (gameObject.CompareTag("Tower"))
        {
            //GameOver
            Manager.GetComponent<Player>().GameOver();
        }

        if (GetComponent<ButtonInfo>() && GetComponent<ButtonInfo>().RangeVisual != null)
        {
            GameObject Visual = GetComponent<ButtonInfo>().RangeVisual;
            Manager.GetComponent<Arrays>().VisualsList.Remove(Visual);
            Destroy(Visual);
        }


        //If gameobject is a building//
        List<GameObject> gos = Manager.GetComponent<Arrays>().BuildingsList;
        if (gos.Contains(gameObject))
        {
            //Shake Camera
            Manager.GetComponent<Player>().CameraShake(0.5f);
            //Remove building from building list
            gos.Remove(gameObject);
        }

        Manager.GetComponent<Arrays>().BuildingDict[Name].Remove(gameObject);

        Instantiate(Particles, transform.position, Particles.transform.rotation);
        Destroy(gameObject);
    }
    
    
    
    //Consider moving this to manager
    //Called On Death, when Scrapped, and Markets making money
    //     health scrap  -  cost*0.75  -  producer scrap
    //          enemies - destroy building - Market
    public void SpawnText(int scrap)
    {
        //Spawn Pop Up Text
        GameObject SpawnedText = Instantiate(TextPopUp, transform.position, Quaternion.identity);
        //Set Text to Scrap
        SpawnedText.transform.GetChild(0).GetComponent<TextMeshPro>().text = "+" + scrap.ToString();
        //Set Text as child of who instantiated it (because it's spawning somewhere else)
        SpawnedText.transform.position = transform.position;

        //Add scrap to player
        PlayerScript.Scrap += scrap;
    }
}
