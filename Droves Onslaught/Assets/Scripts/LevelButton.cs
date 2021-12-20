using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int LevelNumber;

    private TextMeshProUGUI ButtonText;

    //[SerializeField] List<bool> StarsEarned = new List<bool>();



    [SerializeField] bool LevelNumberFromSiblings = true;


    //Is called when enabled 
    private void Start()
    {
        if (LevelNumberFromSiblings)
        {
            //LevelNumber = transform.GetSiblingIndex() + 1;    //Commented out but not deleted because the else statement etc.
        }
        else
        {
            LevelNumber = LevelManager.instance.CurrentLevel;
        }

        SetStars();
    }

    public void SetStars()
    {
        GameObject StarsParent = transform.GetChild(0).GetChild(1).gameObject;

        if (LevelNumberFromSiblings)
        {
            ButtonText = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

            ButtonText.text = LevelNumber.ToString();
        }

        //StarsEarned = LevelManager.instance.Stars[LevelNumber];


        if (LevelManager.instance.Stars[LevelNumber][0])
        {
            StarsParent.transform.GetChild(0).GetComponent<Image>().color = Color.black;


            for (int i = 0; i < 3; i++)
            {
                if (LevelManager.instance.Stars[LevelNumber][i])
                {
                    StarsParent.transform.GetChild(i).GetComponent<Image>().color = Color.black;
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
