using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public int Scrap = 0;

    public int Crystal = 0;

    [SerializeField] TextMeshProUGUI UGUI;

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
}
