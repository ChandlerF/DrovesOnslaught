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

    [SerializeField] Sprite EditorSprite;

    private void Start()
    {
        Text = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        Price =  int.Parse(Text.text);

        if(EditorSprite == null)
        {
            SR = Building.GetComponent<SpriteRenderer>();

            transform.GetChild(0).GetComponent<Image>().sprite = SR.sprite;
        }
        else
        {
            transform.GetChild(0).GetComponent<Image>().sprite = EditorSprite;
        }
    }


    public void UnlockBuilding()
    {
        if (LevelManager.instance.TotalStarsEarned >= Price)
        {
            //Unlock
            Building.GetComponent<ButtonInfo>().IsUnlocked = true;

            //Turn Button Off
            GetComponent<Button>().interactable = false;

            //Subtract Stars
            LevelManager.instance.TotalStarsEarned -= Price;
        }
    }
}
