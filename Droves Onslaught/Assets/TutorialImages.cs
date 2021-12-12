using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialImages : MonoBehaviour
{
    [SerializeField] List<Sprite> Images = new List<Sprite>();

    [SerializeField] float Timer;

    private int index = 1;

    private Image MyImage;

    void Start()
    {
        Time.timeScale = 0;   //Maybe don't need to freeze time, just set time counter to 0


        MyImage = GetComponent<Image>();
        MyImage.sprite = Images[0];

        StartCoroutine(Wait());
    }



    IEnumerator Wait()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(Timer);

            if ((index + 1) > Images.Count)
            {
                index = 0;
            }

            MyImage.sprite = Images[index];

            index++;
        }
    }
}
