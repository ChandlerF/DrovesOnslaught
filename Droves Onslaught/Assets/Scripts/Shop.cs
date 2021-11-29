using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject Building;

    private int Price;

    private TextMeshProUGUI Text;

    private SpriteRenderer SR;

    private string Name;

    [SerializeField] Sprite EditorSprite;

    private void Start()
    {
        Name = Building.GetComponent<ButtonInfo>().Name;
        Text = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        Price =  int.Parse(Text.text);



        if (EditorSprite == null)
        {
            SR = Building.GetComponent<SpriteRenderer>();

            transform.GetChild(0).GetComponent<Image>().sprite = SR.sprite;
        }
        else
        {
            transform.GetChild(0).GetComponent<Image>().sprite = EditorSprite;
        }


        if (LevelManager.instance.BuildingsUnlocked[Name])
        {
            //Turn Button Off
            GetComponent<Button>().interactable = false;
        }
    }


    public void UnlockBuilding()
    {
        if (LevelManager.instance.TotalStarsEarned >= Price)
        {
            //Unlock
            LevelManager.instance.BuildingsUnlocked[Name] = true;

            //Turn Button Off
            GetComponent<Button>().interactable = false;

            //Subtract Stars
            LevelManager.instance.TotalStarsEarned -= Price;

            GameObject TextGO = transform.parent.parent.parent.GetChild(0).GetChild(0).gameObject;
            TextGO.GetComponent<StarsCountText>().SetStarsCount();
        }
    }
}
