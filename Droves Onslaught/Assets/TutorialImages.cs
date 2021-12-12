using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialImages : MonoBehaviour
{
    [SerializeField] List<Sprite> Images = new List<Sprite>();

    [SerializeField] float StartTimer;
    private float Timer;

    private int index = 1;

    private Image MyImage;

    void Start()
    {
        Time.timeScale = 0;

        Timer = StartTimer;

        MyImage = GetComponent<Image>();
        MyImage.sprite = Images[0];
    }

    //Set image to sprite after timer


    void Update()
    {
        if(Timer > 0)   //Broken, timeScale is 0 which makes timer irrelevant, use Co routine? or a weird work around with unscaledTime
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            if ((index + 1) > Images.Count)
            {
                index = 0;
            }

            MyImage.sprite = Images[index];

            index ++;
            Timer = StartTimer;
        }
    }
}
