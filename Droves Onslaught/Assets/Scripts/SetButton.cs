using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetButton : MonoBehaviour
{
    //Set by BuildingButton Script
    public GameObject Building;
    private ButtonInfo Info;

    private TextMeshProUGUI UGUI;      //Text

    [SerializeField] Image Img;

    private Player PlayerScript;

    void Start()
    {
        Info = Building.GetComponent<ButtonInfo>();

        UGUI = GetComponentInChildren<TextMeshProUGUI>();
        //Img = GetComponentInChildren<Image>();        //Didn't work, grabbed img from self


        Img.transform.localScale = Info.Scale;


        Img.sprite = Info.Sprite;
        UGUI.text = Info.Cost.ToString();

        //UGUI.text = Info.Name + " - " + Info.Cost;

        PlayerScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Player>();
    }


    private void Update()
    {
        int Scrap = PlayerScript.Scrap;
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
