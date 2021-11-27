using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    private int LevelNumber;

    private TextMeshProUGUI ButtonText;



    private void Start()
    {
        LevelNumber = transform.GetSiblingIndex() + 1;

        ButtonText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

        ButtonText.text = LevelNumber.ToString();
    }


    private void SetStars()
    {
        if (LevelManager.instance.Stars[LevelNumber][0])
        {
            transform.GetChild(1).gameObject.SetActive(true);


            if (LevelManager.instance.Stars[LevelNumber][1])
            {
                transform.GetChild(1).gameObject.SetActive(true);


                if (LevelManager.instance.Stars[LevelNumber][2])
                {
                    transform.GetChild(1).gameObject.SetActive(true);
                }
            }
        }
        
    }


    public void LoadScene()
    {
        SceneManager.LoadScene("Level" + LevelNumber.ToString());
    }
}
