using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] int HP;

    [SerializeField] GameObject Particles;

    [SerializeField] int Scrap;
    
    [SerializeField] GameObject TextPopUp;
    
    private GameObject Manager;
    
    private Player PlayerScript;

    private void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("Manager");
        
        PlayerScript = Manager.GetCompononent<Player>();
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
        GameObject Manager = GameObject.FindGameObjectWithTag("Manager");
           
        //If on death, it's scrap is more than 0, add it
        if(Scrap > 0)
        {
            SpawnText(Scrap);
        }


        List<GameObject> gos = Manager.GetComponent<Arrays>().BuildingsList; 
        if (gos.Contains(gameObject))
        {
            Manager.GetComponent<Player>().CameraShake(0.5f);
            gos.Remove(gameObject);
        }

        if (gameObject.CompareTag("Tower"))
        {
            //GameOver
            Manager.GetComponent<Player>().Pause();
        }

        if (GetComponent<ButtonInfo>())
        {
            GameObject Visual = GetComponent<ButtonInfo>().RangeVisual;
            Manager.GetComponent<Arrays>().VisualsList.Remove(Visual);
            Destroy(Visual);
        }



        Manager.GetComponent<Arrays>().BuildingDict[gameObject.name.Remove(gameObject.name.Length - 7)].Remove(gameObject);

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
        GameObject SpawnedText = Instantiate(TextPopUp, transform.position, transform.rotation);
        //Set Text to Scrap
        SpawnedText.GetComponent<TextMeshPro>().text = "+" + MarketScrap.ToString();
        //Set Text as child of who instantiated it (because it's spawning somewhere else)
        SpawnedText.transform.SetParent(transform, true);
        
        //Add scrap to player
        PlayerScript.Scrap += scrap
    }
}
