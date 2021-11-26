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
