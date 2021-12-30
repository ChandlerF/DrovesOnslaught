using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterButton : MonoBehaviour
{
    [SerializeField] GameObject ChapterManager;
    private ChaptersManager ManagerScript;

    private bool CanGoForwards = true;
    private bool CanGoBackwards = false;

    private void Start()
    {
        ManagerScript = ChapterManager.GetComponent<ChaptersManager>();

        SetActive();
    }


    public void Forwards()
    {
        SetActive();

        if (CanGoForwards)
        {
            ManagerScript.SetStars(true);
        }
    }
    
    public void Backwards()
    {
        SetActive();

        if (CanGoBackwards)
        {
            ManagerScript.SetStars(false);
        }
    }

    
    private void SetActive()
    {
        if(ManagerScript.ActiveChapter > 0)
        {
            CanGoBackwards = true;
        }
        else
        {
            CanGoBackwards = false;
        }



        if(ManagerScript.ActiveChapter + 2 > LevelManager.instance.TotalChapters)
        {
            CanGoForwards = false;
        }
        else
        {
            CanGoForwards = true;
        }
    }
}
