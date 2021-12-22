using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int StartHP;
    public int HP = 0;

    [SerializeField] GameObject Particles;

    [SerializeField] int Scrap;
    
    [SerializeField] GameObject TextPopUp;
    
    private GameObject Manager;
    
    private Player PlayerScript;
    
    private bool IsTower = false;
    private bool IsBuilding = false;
    private bool IsEnemy = false;

    private void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("Manager");
        
        PlayerScript = Manager.GetComponent<Player>();

        HP = StartHP;
        
        if (gameObject.CompareTag("Tower"))
        {
            IsTower = true;
        }
        else if (gameObject.layer == 6)     //Buildings layer
        {
            IsBuilding = true;
        }
        else if (transform.CompareTag("Enemy"))
        {
            IsEnemy = true;
        }
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
        
        if(IsTower)
        {
            //Shake Camera
            Manager.GetComponent<Player>().CameraShake(0.2f);

            AudioManager.instance.Play("Hit1");
        }
        else if (IsBuilding)
        {
            AudioManager.instance.Play("Hit1");
        }
    }


    private void Death()
    {
        string Name = GetComponent<ButtonInfo>().Name;
        MoveTowards MoveScript = GetComponent<MoveTowards>();
        Arrays ListScript = Manager.GetComponent<Arrays>();

        //If on death, it's scrap is more than 0, add it
        if (Scrap > 0)
        {
            SpawnText(Scrap);
        }

        if (GetComponent<ButtonInfo>() && GetComponent<ButtonInfo>().RangeVisual != null)
        {
            GameObject Visual = GetComponent<ButtonInfo>().RangeVisual;
            ListScript.VisualsList.Remove(Visual);
            Destroy(Visual);
        }


        //If gameobject is a building//
        List<GameObject> gos = ListScript.BuildingsList;
        if (gos.Contains(gameObject))
        {
            //Shake Camera
            Manager.GetComponent<Player>().CameraShake(0.5f);
            ListScript.NumOfBuildingsDestroyed += 1;
            gos.Remove(gameObject);
        }

        ListScript.BuildingDict[Name].Remove(gameObject);

        Instantiate(Particles, transform.position, Particles.transform.rotation);





        if (IsTower)
        {
            AudioManager.instance.Play("Destroyed1");
            Manager.GetComponent<Player>().GameLost();
        }
        else if(IsBuilding)
        {
            AudioManager.instance.Play("Destroyed1");
        }
        else if (IsEnemy)
        {
            AudioManager.instance.Play("Destroyed2");
        }
        
        SpawnBody();

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
    
    
    
    private void SpawnBody()
    {
     GameObject SpawnedBody = Instantiate(new GameObject(), transform.position, transform.rotation);
     SpawnedBody.transform.localScale = Vector3.Scale(transform.localScale, new Vector3(0.5f, 0.5f, 0.5f));
     
     SpriteRenderer sr = SpawnedBody.AddComponent<SpriteRenderer>();
     sr.sprite = GetComponent<SpriteRenderer>().sprite;
     sr.color = Color.black;
    }
}
