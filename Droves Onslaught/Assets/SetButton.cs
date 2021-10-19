using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetButton : MonoBehaviour
{
    public GameObject Building;
    private ButtonInfo Info;

    private TextMeshProUGUI UGUI;      //Text

    [SerializeField] Image Img;

    void Start()
    {
        Info = Building.GetComponent<ButtonInfo>();

        UGUI = GetComponentInChildren<TextMeshProUGUI>();
        //Img = GetComponentInChildren<Image>();        //Didn't work, grabbed img from self

        Img.sprite = Info.Sprite;
        UGUI.text = Info.Name + " - " + Info.Cost;
    }


    private void Update()
    {
        int Scrap = GameObject.FindGameObjectWithTag("Manager").GetComponent<Player>().Scrap;
        if(Scrap >= Info.Cost)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
