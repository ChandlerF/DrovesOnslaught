using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Player : MonoBehaviour
{
    public int Scrap = 0;

    public int Points = 0;

    public bool UsesPoints = false;

    [SerializeField] TextMeshProUGUI UGUI;

    [SerializeField] GameObject PauseMenu;

    [SerializeField] GameObject GameOverMenu;

    private CameraShake CamShake;
    

    private bool GameIsOver = false;

    private Arrays ListScript;

    private void Start()
    {
        CamShake = Camera.main.transform.parent.GetComponent<CameraShake>();

        ListScript = GetComponent<Arrays>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))        ////////////////////////
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
        }

        UGUI.text = Scrap.ToString();
    }


    public void AddScrap(int x)
    {
        Scrap += x;
    }

    public void CameraShake(float x)
    {
        CamShake.Trauma += x;
    }
    
    public void GameOver()
    {
        if (!GameIsOver)
        {
            EnemySpawner Spawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();

            //Star for beating the level
            LevelManager.instance.StarsEarned(0);

            //Another star from beating the level without losing a building
            if (ListScript.NumOfBuildingsDestroyed == 0)
            {
                LevelManager.instance.StarsEarned(1);
            }

            //Another star from beating the level a second time, but on hard mode
            if (Spawner.FluctuateSpawnRate == true)
            {
                LevelManager.instance.StarsEarned(2);
            }


            LevelManager.instance.SavePlayer();


            Time.timeScale = 0;


            GameOverMenu.SetActive(true);

            GameIsOver = true;
        }
    }

    public void GameLost()
    {
        if (!GameIsOver)
        {
            Time.timeScale = 0;
            
            GameOverMenu.SetActive(true);

            GameIsOver = true;
        }
    }





    //go = who called the func
    //Spawns an image, need the go to set the image in a canvas (if i spawn it on a canvas you can't unpause with button)
    public void Pause(GameObject go)
    {
        if(!ListScript.IsPaused)
        {
            PauseMenu.SetActive(true);

            Time.timeScale = 0;
            ListScript.IsPaused = true;
        }
        else
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
            ListScript.IsPaused = false;
        }
    }
}
