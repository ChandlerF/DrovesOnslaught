using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    private int LevelNumber;

    private TextMeshProUGUI ButtonText;

    //[SerializeField] List<bool> StarsEarned = new List<bool>();


    [SerializeField] Color GoldColor;


    private void Start()
    {
        LevelNumber = transform.GetSiblingIndex() + 1;

        ButtonText = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

        ButtonText.text = LevelNumber.ToString();
        SetStars();
    }


    private void SetStars()
    {
        GameObject StarsParent = transform.GetChild(0).GetChild(1).gameObject;


        //StarsEarned = LevelManager.instance.Stars[LevelNumber];


        if (LevelManager.instance.Stars[LevelNumber][0])
        {
            StarsParent.transform.GetChild(0).GetComponent<Image>().color = GoldColor;


            for (int i = 0; i < 3; i++)
            {
                if (LevelManager.instance.Stars[LevelNumber][i])
                {
                    StarsParent.transform.GetChild(i).GetComponent<Image>().color = GoldColor;
                }
            }
        }
    }


    public void LoadScene()
    {
        LevelManager.instance.CurrentLevel = LevelNumber;

        SceneManager.LoadScene("Level" + LevelNumber.ToString());
    }
}
