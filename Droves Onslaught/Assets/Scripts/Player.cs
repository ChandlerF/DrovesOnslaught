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

    private CameraShake CamShake;

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

        if (Input.GetKeyDown(KeyCode.O))
        {
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 1;
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
    
    
    public void Pause()
    {
        //Spawn a dim canvas for this, pause menu, and InTetherMode
        Instantiate(GetComponent<PlacingBuildings>().DimCanvas, transform.position, transform.rotation);
        GetComponent<Arrays>().IsPaused = true;
        Time.timeScale = 0;
    }
}
