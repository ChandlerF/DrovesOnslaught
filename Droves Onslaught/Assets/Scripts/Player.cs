using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public int Scrap = 0;

    public int Points = 0;

    [SerializeField] TextMeshProUGUI UGUI;

    [SerializeField] GameObject PauseMenu;

    [SerializeField] GameObject GameOverMenu;

    private CameraShake CamShake;
    

    private bool GameIsOver = false;

    private void Start()
    {
        CamShake = Camera.main.transform.parent.GetComponent<CameraShake>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))        ////////////////////////
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            //1 star from winning
            int StarsEarned = 1;
            EnemySpawner Spawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();

            //Another star from beating the level a second time, but on hard mode
            if (Spawner.FluctuateSpawnRate == true)
            {
                StarsEarned += 1;
            }

            //Another star from beating the level without losing a building
            if (GetComponent<Arrays>().NumOfBuildingsDestroyed == 0)
            {
                StarsEarned += 1;
            }




            //Tell Level Manager Stars Earned
            LevelManager.instance.StarsEarned(StarsEarned);

            Time.timeScale = 0;

            //Set Time to 1

            GameOverMenu.SetActive(true);
            //SceneManager.LoadScene("LevelSelect");
            GameIsOver = true;
        }
    }


    //go = who called the func
    //Spawns an image, need the go to set the image in a canvas (if i spawn it on a canvas you can't unpause with button)
    public void Pause(GameObject go)
    {
        if(!GetComponent<Arrays>().IsPaused)
        {
            PauseMenu.SetActive(true);

            Time.timeScale = 0;
            GetComponent<Arrays>().IsPaused = true;
        }
        else
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
            GetComponent<Arrays>().IsPaused = false;
        }
    }
}
