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
    
    private GameObject SpawnedDimCanvas;

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

        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
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
        if(!GetComponent<Arrays>().IsPaused)
        {
            //Spawn a dim canvas for this, pause menu, and InTetherMode
            SpawnedDimCanvas = Instantiate(GetComponent<PlacingBuildings>().DimCanvas, transform.position, transform.rotation);
            Time.timeScale = 0;
            GetComponent<Arrays>().IsPaused = true;
        }
        else
        {
            Destroy(SpawnedDimCanvas);
            Time.timeScale = 1;
            GetComponent<Arrays>().IsPaused = false;
        }
    }
}
